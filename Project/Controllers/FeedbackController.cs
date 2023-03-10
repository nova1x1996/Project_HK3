using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System.Security.Claims;

namespace Project.Controllers
{
    [Authorize(Roles = "customer")]
    public class FeedbackController : Controller
    {
        private DatabaseContext db;
        public INotyfService notyfService;
        public FeedbackController(DatabaseContext _db, INotyfService _notyfService)
        {
            db = _db;
            notyfService = _notyfService;
        }

        public IActionResult Create()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                notyfService.Error("You must be login !");
                return RedirectToAction("Home", "Index");
            }


            var customer = db.Customers.Where(c => c.user_id.Equals(userId)).SingleOrDefault();
            ViewBag.cus = customer.id;
            return View();
        }

        [HttpPost]

        public IActionResult Create(Feedback model)
        {

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = db.Customers.Where(c => c.user_id.Equals(userId)).SingleOrDefault();
            ViewBag.cus = customer.id;

            if (ModelState.IsValid)
            {


                model.date = DateTime.Now;
                db.Feed_Backs.Add(model);
                db.SaveChanges();
                notyfService.Success("Feedback has been sent !");
                return RedirectToAction("DetailCustomer","UserAuthentication");
            }
            return View();
        }
    }
}
