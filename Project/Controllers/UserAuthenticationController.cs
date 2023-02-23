using Project.Models.DTO;
using Project.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Repositories.Implementation;
using Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Project.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _authService;
        private readonly DatabaseContext db;
        public UserAuthenticationController(IUserAuthenticationService authService,DatabaseContext _db)
        {
            this._authService = authService;
            db = _db;
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
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = result.Message;
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
            if (!ModelState.IsValid) { return View(model); }
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
            return RedirectToAction(nameof(Registration));
            // return RedirectToAction(nameof(Registration));
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
                FirstName = "Do Chi",
                LastName = "Tai",
                Password = "Admin@12345#"
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
              return View(model);
            var result = await _authService.ChangePasswordAsync(model, User.Identity.Name);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(ChangePassword));
        }


        public ActionResult DetailCustomer()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = db.Customers.Include(r => r.ApplicationUser)
                .SingleOrDefault(c => c.user_id.Equals(userId));

            return View(model);
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
    }
}
