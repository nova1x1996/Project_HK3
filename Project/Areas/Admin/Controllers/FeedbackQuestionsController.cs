using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ess;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class FeedbackQuestionsController : Controller
    {

        private DatabaseContext db;
        public INotyfService notyfService;
        public FeedbackQuestionsController(DatabaseContext _db, INotyfService _notyfService)
        {
            db = _db;
            notyfService = _notyfService;
        }

        public ActionResult Index()
        {
            var model = db.Feed_Backs.Include(f => f.GetCustomer.ApplicationUser).ToList();
            return View(model);
        }

        
        public ActionResult Details(int id)
        {
            var model = db.Feed_Backs.Include(f => f.GetCustomer.ApplicationUser).Where(f => f.id == id).FirstOrDefault();
            return View(model);
        }

  

        // GET: FeedbackQuestionsController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = db.Feed_Backs.Find(id);
            if(model != null)
            {
                db.Feed_Backs.Remove(model);
                db.SaveChanges();
                notyfService.Success("Delete successfully!");
                return RedirectToAction("Index");
            }
            notyfService.Error("Delete unsuccessful !");
            return RedirectToAction("Index");
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
