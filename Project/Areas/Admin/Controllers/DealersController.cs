using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DealersController : Controller
    {
        // GET: DealersController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DealersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DealersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DealersController/Create
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

        // GET: DealersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DealersController/Edit/5
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

        // GET: DealersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DealersController/Delete/5
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
