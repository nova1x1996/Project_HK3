using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Runtime.InteropServices;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PackagesController : Controller
    {
        public DatabaseContext db { get; set; }
        public PackagesController(DatabaseContext _db)
        {
            db = _db;
        }

        // GET: PackagesController
        public ActionResult Index()
        {
            var model = db.Packages.ToList();
            return View(model);
        }

        // GET: PackagesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PackagesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PackagesController/Create
        [HttpPost]
        public ActionResult Create(Package newPackage)
        {
      

            try
            {
                if (ModelState.IsValid)
                {
                    db.Packages.Add(newPackage);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ModelState.ErrorCount.ToString());
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
            return View();
        }

        // POST: PackagesController/Edit/5
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

        // GET: PackagesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PackagesController/Delete/5
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
