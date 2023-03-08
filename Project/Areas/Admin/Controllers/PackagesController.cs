using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class PackagesController : Controller
    {
        public DatabaseContext db { get; set; }
        public INotyfService notyfService { get; }
        public PackagesController(DatabaseContext _db, INotyfService _notyfService)
        {
            db = _db;
            notyfService = _notyfService;
        }

        // GET: PackagesController
        public ActionResult Index()
        {
            var model = db.Packages.Include(p => p.customers).ToList();
            return View(model);
        }

        // GET: PackagesController/Details/5
        public ActionResult Details(int id)
        {
            var model = db.Packages.Include(p => p.customers).SingleOrDefault(m => m.id.Equals(id));
            return View(model);
        }

        // GET: PackagesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PackagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Package newPackage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Packages.Add(newPackage);
                    db.SaveChanges();
                    notyfService.Success("Create new successfully");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "New creation failed.\r\nPlease enter full information.");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        // GET: PackagesController/Edit/5
        public ActionResult Edit(int id)
        {
            var pac = db.Packages.SingleOrDefault(e => e.id.Equals(id));
            return View(pac);
        }

        // POST: PackagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Package package)
        {
            try
            {
                var pac = db.Packages.SingleOrDefault(e => e.id.Equals(package.id));
                if (pac != null && ModelState.IsValid)
                {
                    pac.name = package.name;
                    pac.duration = package.duration;
                    pac.details = package.details;
                    pac.status = package.status;
                    pac.price = package.price;
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
            return View();
        }

        // GET: PackagesController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var model = db.Packages.SingleOrDefault(m => m.id.Equals(id));
                if (model != null)
                {
                    db.Packages.Remove(model);
                    db.SaveChanges();
					notyfService.Success("Delete successfully");
					return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }
    }
}
