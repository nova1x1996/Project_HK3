using AspNetCoreHero.ToastNotification.Notyf;
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

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DealersController : Controller
    {
        private readonly IUserAuthenticationService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        public DatabaseContext db { get; set; }
        public DealersController(IUserAuthenticationService authService, DatabaseContext _db, UserManager<ApplicationUser> userManager)
        {
            this._authService = authService;
            db = _db;
            _userManager = userManager;
        }
        // GET: DealersController
        public ActionResult Index()
        {
            var model = db.Dealers
                .Include(d => d.ApplicationUser)
                .ToList();
            return View(model);
        }

        // GET: DealersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DealersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DealersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistrationModel model, string phone, string address)
        {
            if (!ModelState.IsValid) { return View(model); }
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

            }
            return RedirectToAction("Index");
        }

        // GET: DealersController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var deal = db.Dealers.SingleOrDefault(d => d.id.Equals(id));
            var userManager = HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByIdAsync(deal.user_id);
            return View(user);
        }

        // POST: DealersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Dealers dealers, string newPassword)
        {
            try
            {
                var deal = db.Dealers.SingleOrDefault(d => d.id.Equals(dealers.id));
                var userManager = HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();
                var user = await userManager.FindByIdAsync(deal.user_id);
                if (user != null && ModelState.IsValid)
                {
                    // Tạo mật khẩu mới cho tài khoản ApplicationUser
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await userManager.ResetPasswordAsync(user, token, newPassword);

                    if (result.Succeeded)
                    {
                        // Lưu thay đổi vào cơ sở dữ liệu
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Edit failed.\r\nPlease correct valid information.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }




        // GET: DealersController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dealer = await db.Dealers.FindAsync(id);
                if (dealer == null)
                {
                    return NotFound();
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
