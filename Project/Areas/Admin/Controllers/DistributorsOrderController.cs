using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class DistributorsOrderController : Controller
    {
        // GET: DistributorsOrderController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DistributorsOrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DistributorsOrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DistributorsOrderController/Create
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

        // GET: DistributorsOrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DistributorsOrderController/Edit/5
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

        // GET: DistributorsOrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DistributorsOrderController/Delete/5
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
