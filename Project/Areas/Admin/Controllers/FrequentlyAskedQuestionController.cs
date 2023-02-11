using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class FrequentlyAskedQuestionController : Controller
    {
        // GET: FrequentlyAskedQuestionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FrequentlyAskedQuestionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FrequentlyAskedQuestionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FrequentlyAskedQuestionController/Create
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

        // GET: FrequentlyAskedQuestionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FrequentlyAskedQuestionController/Edit/5
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

        // GET: FrequentlyAskedQuestionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FrequentlyAskedQuestionController/Delete/5
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
