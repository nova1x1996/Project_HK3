using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class ContactController : Controller
    {
        private DatabaseContext db;
        public INotyfService notyfService { get; }
        public ContactController(DatabaseContext _db, INotyfService _notyfService)
        {
            db = _db;
            notyfService = _notyfService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ContactUs contactUs)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ContactUss.Add(contactUs);
                    db.SaveChanges();
                    notyfService.Success("Submit successfully");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Fail");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }


      
    }
}
