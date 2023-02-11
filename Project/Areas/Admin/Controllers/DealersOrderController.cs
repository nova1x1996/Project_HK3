using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class DealersOrderController : Controller
    {
        // GET: DealersOrderController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DealersOrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DealersOrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DealersOrderController/Create
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

        // GET: DealersOrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DealersOrderController/Edit/5
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

        // GET: DealersOrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DealersOrderController/Delete/5
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
