using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Controllers;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class FrequentlyAskedQuestionsController : Controller
    {
        public INotyfService notyf;
        public DatabaseContext db { get; set; }
        public FrequentlyAskedQuestionsController(DatabaseContext _db,INotyfService _notyf)
        {
            db = _db;
            notyf = _notyf;
        }
        // GET: FrequentlyAskedQuestionsController
        public ActionResult Index()
        {
            var model = db.Faq.ToList();
            return View(model);
        }

        // GET: FrequentlyAskedQuestionsController/Details/5
        public ActionResult Details(int id)
        {
            var model = db.Faq.SingleOrDefault(m => m.id.Equals(id));
            return View(model);
        }

        // GET: FrequentlyAskedQuestionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FrequentlyAskedQuestionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Faq newFaq)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Faq.Add(newFaq);
                    db.SaveChanges();
                    notyf.Success("You creation was successful!");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Your creation was unsuccessful ");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        // GET: FrequentlyAskedQuestionsController/Edit/5
        public ActionResult Edit(int id)
        {
            var FAQ = db.Faq.SingleOrDefault(e => e.id.Equals(id));
            return View(FAQ);
        }

        // POST: FrequentlyAskedQuestionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Faq faq)
        {
            try
            {
                var FAQ = db.Faq.SingleOrDefault(e => e.id.Equals(faq.id));
                if (FAQ != null && ModelState.IsValid)
                {
                    FAQ.id = faq.id;
                    FAQ.question = faq.question;
                    FAQ.answer = faq.answer;
                    FAQ.status = faq.status;
                    db.SaveChanges();
                    notyf.Success("You edit was successful!");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Edit failed.\r\nPlease correct valid information.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        // GET: FrequentlyAskedQuestionsController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var FAQQ = db.Faq.SingleOrDefault(m => m.id.Equals(id));
                if (FAQQ != null)
                {
                    db.Faq.Remove(FAQQ);
                    db.SaveChanges();
                    notyf.Success("You Delete was successful!");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
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
