using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoviesCategoryController : Controller
    {
        // GET: MoviesCategoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MoviesCategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MoviesCategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MoviesCategoryController/Create
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

        // GET: MoviesCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MoviesCategoryController/Edit/5
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

        // GET: MoviesCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MoviesCategoryController/Delete/5
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
