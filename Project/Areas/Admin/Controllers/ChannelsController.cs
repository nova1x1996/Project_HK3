using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChannelsController : Controller
    {
        // GET: ChannelsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ChannelsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChannelsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChannelsController/Create
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

        // GET: ChannelsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChannelsController/Edit/5
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

        // GET: ChannelsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChannelsController/Delete/5
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
