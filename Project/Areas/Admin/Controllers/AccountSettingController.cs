using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class AccountSettingController : Controller
    {
        // GET: AccountSettingController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountSettingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountSettingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountSettingController/Create
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

        // GET: AccountSettingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountSettingController/Edit/5
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

        // GET: AccountSettingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountSettingController/Delete/5
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
