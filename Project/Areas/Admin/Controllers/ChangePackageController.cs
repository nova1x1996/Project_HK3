using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChangePackageController : Controller
    {
        private DatabaseContext db;
        private INotyfService notyf;

        public ChangePackageController(DatabaseContext _db,INotyfService _notyf)
        {
            db = _db;
            notyf = _notyf;
        }
        public IActionResult Index()
        {
            var model = db.ChangePackages.Include(c=>c.GetCustomer).ThenInclude(c=>c.ApplicationUser).ToList();
            var package = db.Packages.ToList();
            ViewBag.package = package;
            return View(model);
        }

        [HttpGet]
        public IActionResult ConfirmPayment(int id)
        {
            var CP = db.ChangePackages.Find(id);
            var cus = db.Customers.Find(CP.customer_id);
            var package = db.Packages.Find(CP.packageNew);

            if (cus.package_id != null)
            {
                cus.package_id = CP.packageNew;
                cus.payment_monthly = package.price;
                CP.state = true;
                db.SaveChanges();
                notyf.Success("Update Package Successfully !");
                return RedirectToAction("Index");
            }

            notyf.Error("You cannot update package !");
            return RedirectToAction("Index");
           
        }
    }
}
