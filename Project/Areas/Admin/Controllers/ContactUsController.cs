using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController : Controller
    {
        // GET: ContactUsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ContactUsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContactUsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactUsController/Create
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

        // GET: ContactUsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContactUsController/Edit/5
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

        // GET: ContactUsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContactUsController/Delete/5
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
