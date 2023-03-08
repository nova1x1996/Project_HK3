using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ess;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
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

            var model = db.Location.Where(l => l.Name.Equals(newlocation.Name)).SingleOrDefault();
            if(model != null)
            {
                notyfService.Error("The name already exists");
                return RedirectToAction("Index");
            }
            
                if (ModelState.IsValid)
                {
                    db.Location.Add(newlocation);
                    db.SaveChanges();
                    notyfService.Success("Create new successfully");
                    return RedirectToAction("Index");
                }
                
            return View();
        }

        // GET: LocationController/Edit/5
        public ActionResult Edit(int id)
        {
            var FAQ = db.Location.SingleOrDefault(e => e.Id == id);
            return View(FAQ);
         
        }

        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Location location)
        {
            try
            {
                var model = db.Location.Where(l => l.Name.Equals(location.Name)).SingleOrDefault();
                if (model != null)
                {
                    notyfService.Error("The name already exists");
                  
                    return View();
                }


                var Location = db.Location.SingleOrDefault(e => e.Id == location.Id);
                if (Location != null && ModelState.IsValid)
                {                    
                    Location.Name = location.Name;
                    db.SaveChanges();
                    notyfService.Success("You edit was successful!");
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
            return View();
        }

        // GET: LocationController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var lcExists = db.CustomerCare.Where(c => c.location_id == id).FirstOrDefault();
                if(lcExists != null)
                {
                    notyfService.Error("You can only delete Location without CustomerCare.");
                    return RedirectToAction("Index");
                }
                
                var lc = db.Location.SingleOrDefault(m => m.Id.Equals(id));
                if (lc != null)
                {
                    db.Location.Remove(lc);
                    db.SaveChanges();
                    notyfService.Success("You Delete was successful!");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
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
