using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class DistributorsController : Controller
    {
        // GET: DistributorsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DistributorsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DistributorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DistributorsController/Create
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

        // GET: DistributorsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DistributorsController/Edit/5
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

        // GET: DistributorsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DistributorsController/Delete/5
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
