using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.Domain;
using Project.Models.DTO;
using Project.Repositories.Abstract;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DealersController : Controller
    {
        private readonly IUserAuthenticationService _authService;
        private UserManager<ApplicationUser> _userManager;
		public INotyfService notyfService { get; }
		public DatabaseContext db { get; set; }
        public DealersController(IUserAuthenticationService authService, INotyfService _notyfService, DatabaseContext _db, UserManager<ApplicationUser> userManager)
        {
            this._authService = authService;
            db = _db;
            _userManager = userManager;
			notyfService = _notyfService;
		}
		// GET: DealersController
		[Authorize(Roles = "admin")]
		public ActionResult Index()
        {
            var model = db.Dealers
                .Include(d => d.ApplicationUser)
                .ToList();
            return View(model);
        }

		[Authorize(Roles = "dealer")]
		[HttpGet]
		public IActionResult ChangePassword()
		{
			return View();
		}

		[Authorize(Roles = "dealer")]
		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);

			}

			if (string.Compare(model.CurrentPassword, model.NewPassword) == 0)
			{
				TempData["Error"] = "The new password cannot be the same as the old password.";
			}
			else
			{
				var result = await _authService.ChangePasswordAsync(model, User.Identity.Name);


				if (result.Message.Equals("Some error occcured"))
				{
					TempData["Error"] = "Current password is incorrect";

				}
				else
				{
					TempData["success"] = result.Message;
				}
			}
			return RedirectToAction(nameof(ChangePassword));

		}

		// GET: DealersController/Create
		[Authorize(Roles = "admin")]
		public ActionResult Create()
        {
            return View();
        }

		// POST: DealersController/Create
		[Authorize(Roles = "admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistrationModel model, string phone, string address)
        {
            if (!Regex.IsMatch(phone, @"^\d+$"))
            {
                TempData["Error"] = "The phone field must be numeric.";
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingUser = await db.Users.AnyAsync(u => u.UserName == model.Username);
            if (existingUser)
            {
                ModelState.AddModelError("Username", "Username already exists.");
                return View(model);
            }

            model.Role = "dealer";

            var result = await this._authService.RegisterAsync(model);

            TempData["msg"] = result.Message;

            if (result.Message.Equals("You have registered successfully"))
            {
                var u1 = await db.Users.SingleOrDefaultAsync(u => u.UserName.Equals(model.Username));
                var d1 = new Dealers();
                d1.phone = phone;
                d1.address = address;
                d1.user_id = u1.Id;
                await db.Dealers.AddAsync(d1);
                await db.SaveChangesAsync();
                notyfService.Success("Create new successfully");

			}

            return RedirectToAction("Index");
        }


		// GET: DealersController/Edit/5
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Edit(int id)
        {
            var deal = db.Dealers.SingleOrDefault(d => d.id.Equals(id));
            return View(deal);
        }

        // POST: DealersController/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string dealers_id, string newPassword)
        {
            var deal = db.Dealers.Find(int.Parse(dealers_id));
            var user = await _userManager.FindByIdAsync(deal.user_id);

            if (!IsValidPassword(newPassword))
            {
                TempData["Error"] = "Password must contain at least one uppercase letter, one lowercase letter, one number, one special character, and be at least 6 characters long.";
                return View(deal);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            try
            {
                if (result.Succeeded)
                {
                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();
                    notyfService.Success("Edit successfully");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Edit failed.\r\nPlease correct valid information.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(deal);
        }

        private bool IsValidPassword(string password)
        {
            // Kiểm tra password phải có ít nhất 8 ký tự và chứa ít nhất một ký tự hoa, một ký tự thường, một ký tự số và một ký tự đặc biệt
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$");
            return regex.IsMatch(password);
        }




        // GET: DealersController/Delete/5
        [Authorize(Roles = "admin")]
		public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dealer = await db.Dealers.FindAsync(id);
                if (dealer == null)
                {
                    return NotFound();
                }

                var hasOrders = db.Dealer_Orders.Any(d => d.dealers_id == id);
                if (hasOrders)
                {
                    notyfService.Error("This dealer cannot be deleted because they have placed orders.");
                    return RedirectToAction("Index");
                }

                db.Dealers.Remove(dealer);

                var user = await _userManager.FindByIdAsync(dealer.user_id);

                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);

                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to delete user.");
                    }
                }

                await db.SaveChangesAsync();
				notyfService.Success("Delete successfully");
				return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

    }
}
