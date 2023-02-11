using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class CustomersOrderController : Controller
    {
        // GET: CustomersOrderController
        public ActionResult Index()
        {
            return View();
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
