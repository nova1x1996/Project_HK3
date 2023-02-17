using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersOrderController : Controller
    {

        private DatabaseContext db;
        public CustomersOrderController(DatabaseContext _db)
        {
            db = _db;
        }

        // GET: CustomersOrderController
        public ActionResult Index()
        {
            var model = db.Customer_orders
                .Include(c => c.GetCustomer)
                    .ThenInclude(c=>c.ApplicationUser)
                .Include(c=>c.GetMovie)
                .Include(c => c.GetSetUpBox)
                .Include(c => c.GetPackage)
                .ToList();
            return View(model);
        }

        // GET: CustomersOrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersOrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersOrderController/Create
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

        // GET: CustomersOrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomersOrderController/Edit/5
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

        // GET: CustomersOrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomersOrderController/Delete/5
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
