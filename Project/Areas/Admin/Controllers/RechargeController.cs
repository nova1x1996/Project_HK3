using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class RechargeController : Controller
    {
        // GET: RechargeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RechargeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RechargeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RechargeController/Create
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

        // GET: RechargeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RechargeController/Edit/5
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

        // GET: RechargeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RechargeController/Delete/5
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
