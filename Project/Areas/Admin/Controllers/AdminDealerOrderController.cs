using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AdminDealerOrderController : Controller
    {
        public INotyfService notyfService { get; }
        public DatabaseContext db { get; set; }
        public AdminDealerOrderController(DatabaseContext _db, INotyfService _notyfService)
        {
            db = _db;
            notyfService = _notyfService;
        }
        public IActionResult Index()
        {
            var model = db.Dealer_Orders.Include(d => d.GetDealer)
              .ThenInclude(d => d.ApplicationUser)
              .Include(d => d.GetSetUpBox)
              .ToList();

            return View(model);
       
        }


        public IActionResult Confirm(int id)
        {
            var model = db.Dealer_Orders.Find(id);
            model.status = true;
            db.SaveChanges();
            notyfService.Success("Change state order success ");
            return RedirectToAction("Index");

        }
    }
}
