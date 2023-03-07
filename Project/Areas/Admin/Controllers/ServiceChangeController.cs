using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ServiceChangeController : Controller
    {
        // GET: ServiceChangeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ServiceChangeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServiceChangeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceChangeController/Create
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

        // GET: ServiceChangeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ServiceChangeController/Edit/5
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

        // GET: ServiceChangeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ServiceChangeController/Delete/5
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
