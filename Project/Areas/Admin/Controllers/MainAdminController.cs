using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainAdminController : Controller
    {
        // GET: MainAdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MainAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MainAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MainAdminController/Create
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

        // GET: MainAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MainAdminController/Edit/5
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

        // GET: MainAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MainAdminController/Delete/5
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
