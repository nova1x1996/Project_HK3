using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FrequentlyAskedQuestionsController : Controller
    {
        // GET: FrequentlyAskedQuestionsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FrequentlyAskedQuestionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FrequentlyAskedQuestionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FrequentlyAskedQuestionsController/Create
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

        // GET: FrequentlyAskedQuestionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FrequentlyAskedQuestionsController/Edit/5
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

        // GET: FrequentlyAskedQuestionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FrequentlyAskedQuestionsController/Delete/5
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
