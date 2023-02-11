using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class CustomerFeedbackReviewController : Controller
    {
        // GET: CustomerFeedbackReviewController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomerFeedbackReviewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerFeedbackReviewController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerFeedbackReviewController/Create
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

        // GET: CustomerFeedbackReviewController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerFeedbackReviewController/Edit/5
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

        // GET: CustomerFeedbackReviewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerFeedbackReviewController/Delete/5
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
