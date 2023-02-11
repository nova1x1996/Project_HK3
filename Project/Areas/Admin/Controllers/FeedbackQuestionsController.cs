using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeedbackQuestionsController : Controller
    {
        // GET: FeedbackQuestionsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FeedbackQuestionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FeedbackQuestionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FeedbackQuestionsController/Create
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

        // GET: FeedbackQuestionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeedbackQuestionsController/Edit/5
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

        // GET: FeedbackQuestionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FeedbackQuestionsController/Delete/5
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
