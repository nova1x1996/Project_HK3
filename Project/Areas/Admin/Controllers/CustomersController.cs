using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Runtime.InteropServices;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CustomersController : Controller
    {
        public DatabaseContext db { get; set; }
        public CustomersController(DatabaseContext _db)
        {
            db = _db;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            var model = db.Customers.Include(a=>a.package).Include(c=>c.ApplicationUser).ToList();
            return View(model);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            var model = db.Customers.Include(c=>c.package).Include(c=>c.ApplicationUser).FirstOrDefault(c=>c.id == id);

            return View(model);
        }


        [HttpGet]
        public ActionResult ChangeToInvalid(int id)
        {
            var model = db.Customers.Find(id);
            model.statePackage = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChangeToValid(int id)
        {
            var model = db.Customers.Find(id);
            model.statePackage = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
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

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
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
