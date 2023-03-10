using Project.Models.DTO;
using Project.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Repositories.Implementation;
using Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Project.Models.Domain;
using System.Text.RegularExpressions;

namespace Project.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _authService;
        private readonly DatabaseContext db;
        private readonly INotyfService _notyf;
        private  UserManager<ApplicationUser> userManager;
        public UserAuthenticationController(IUserAuthenticationService authService,DatabaseContext _db, INotyfService notyf, UserManager<ApplicationUser> _userManager)
        {
            this._authService = authService;
            db = _db;
            _notyf = notyf;
            userManager = _userManager;
        }

        
        public IActionResult Login()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.LoginAsync(model);
            if(result.StatusCode==1)
            {
                if (model.Username == "admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["msgLogin"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model,string card_number,string phone,string address)
        {
            var cardExists = db.Customers.Where(c => c.card_number.Equals(card_number)).FirstOrDefault();
            if (card_number == null || card_number.Length != 8 )
            {
                TempData["Error4"] = "Please enter 8 digits";
                return View();

            }
            if (card_number == null || !Regex.IsMatch(card_number, @"^[0-9]+$")){
                TempData["Error5"] = "The card number must be number";
                return View();
            }
            if (cardExists != null)
            {
                TempData["Error"] = "Card Number already exist.";
                return View();

            }
            if (card_number == null)
            {
                TempData["Error1"] = "The Card number field is required.";
                return View();
            }

            if (phone == null)
            {
                TempData["Error2"] = "The Phone field is required.";
                return View();

            }
            if (phone == null || !Regex.IsMatch(phone, @"^[0-9]+$"))
            {
                TempData["Error6"] = "The phone must be number";
                return View();
            }

            if (address == null)
            {
                TempData["Error3"] = "The Address field is required.";
                return View();

            }

            if (!ModelState.IsValid) 
            { 
                return View(model); 
            }

            model.Role = "customer";
            
            var result = await this._authService.RegisterAsync(model);

            TempData["msg"] = result.Message;
            

            //Thêm vào để add Customer
            if (result.Message.Equals("You have registered successfully"))
            {
                var u1 = await db.Users.SingleOrDefaultAsync(u => u.UserName.Equals(model.Username));

                var c1 = new Customer();
                c1.card_number = card_number;
                c1.phone = phone;
                c1.address = address;
                c1.user_id = u1.Id;
                
                await db.Customers.AddAsync(c1);
                await db.SaveChangesAsync();

            }
             _notyf.Success("You have registered successfully\"");
            return RedirectToAction("Login");
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await this._authService.LogoutAsync();  
            return RedirectToAction("Index","Home");
        }
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAdmin()
        {
            RegistrationModel model = new RegistrationModel
            {
                Username = "admin",
                Email = "admin@gmail.com",
                FirstName = "R-DTH",
                LastName = "Company",
                Password = "Admin@123"
            };
            model.Role = "admin";
            var result = await this._authService.RegisterAsync(model);
            return Ok(result);
        }

        //[Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult>ChangePassword(ChangePasswordModel model)
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


        public ActionResult DetailCustomer()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = db.Customers.Include(r => r.ApplicationUser)
                .SingleOrDefault(c => c.user_id.Equals(userId));

            return View(model);
        }


        public ActionResult Delete(int id)
        {
            var model = db.Customer_orders.Find(id);
            if(model != null && model.state == false)
            {
                db.Customer_orders.Remove(model);
                db.SaveChanges();
                _notyf.Success("You cancel successfully !");
                return RedirectToAction("HistoryCustomerOrder");
            }
          
            return View(HistoryCustomerOrder);
        }
        public ActionResult HistoryCustomerOrder()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customerID = db.Customers
               .SingleOrDefault(c => c.user_id.Equals(userId));

            var model = db.Customer_orders
                .Include(c => c.GetCustomer)
                    .ThenInclude(c => c.ApplicationUser)
                .Include(c => c.GetMovie)
                .Include(c => c.GetSetUpBox)
                .Include(c => c.GetPackage)
                .Where(c => c.customer_id.Equals(customerID.id))
                .ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Save(string firstName, string lastName,string phone,string address,string email)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customer = db.Customers.Where(c => c.user_id.Equals(userId)).SingleOrDefault();
            customer.phone = phone;
            customer.address = address;

            var user = await userManager.FindByIdAsync(userId);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            var result = await userManager.UpdateAsync(user);
            _notyf.Success("Update Success");
            return RedirectToAction("DetailCustomer");
         
            
        }
    }
}
