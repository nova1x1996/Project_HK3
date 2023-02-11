using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChannelsCategoryController : Controller
    {
        // GET: ChannelsCategoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ChannelsCategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChannelsCategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChannelsCategoryController/Create
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

        // GET: ChannelsCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChannelsCategoryController/Edit/5
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

        // GET: ChannelsCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChannelsCategoryController/Delete/5
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
