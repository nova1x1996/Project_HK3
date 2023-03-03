using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationController : Controller
    {
        public DatabaseContext db { get; set; }
        public INotyfService notyfService { get; }
        public LocationController(DatabaseContext _db, INotyfService _notyfService)
        {
            db = _db;
            notyfService = _notyfService;
        }
        // GET: LocationController
        public ActionResult Index()
        {
            var model = db.Location.ToList();
            return View(model);
        }

        // GET: LocationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Location newlocation)
        {
            
            
                if (ModelState.IsValid)
                {
                    db.Location.Add(newlocation);
                    db.SaveChanges();
                    notyfService.Success("Create new successfully");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "New creation failed.\r\nPlease enter full information.");
                }
            return View();
        }

        // GET: LocationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
