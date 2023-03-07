using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Authorization;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ContactUsController : Controller
    {
        private DatabaseContext db;
        public INotyfService notyfService { get; }
        public ContactUsController(DatabaseContext _db, INotyfService _notyfService)
        {
            db = _db;
            notyfService = _notyfService;
        }
        // GET: ContactUsController
        public ActionResult Index()
        {
            var model = db.ContactUss.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult SendEmail(int id)
        {
            ContactUs contactUs = db.ContactUss.Find(id);
            return View(contactUs);
            
        }

        [HttpPost]
        public ActionResult SendEmail(string content, string first_name, string email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 465);
                        client.Authenticate("hoangdeptraibodoiqua4321@gmail.com", "gcbhrgquuqrfwohx");

                        var bodyBuilder = new BodyBuilder
                        {
                            HtmlBody = $"<p>Hi, {first_name}</p><p>We have received your question</p><p>Our answer is {content}</p><p>Thank you for using our service</p>",
                           
                        };

                        var message = new MimeMessage
                        {
                            Body = bodyBuilder.ToMessageBody(),
                        };
                        message.From.Add(new MailboxAddress("R-DTH", "hoangdeptraibodoiqua4321@gmail.com"));
                        message.To.Add(new MailboxAddress("User", email));
                        message.Subject = "Answer customer questions";
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    notyfService.Success("Send email successfully");
                    return RedirectToAction("SendEmail");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "fail");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
          
        }

        // GET: ContactUsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContactUsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactUsController/Create
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

        // GET: ContactUsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContactUsController/Edit/5
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

        // GET: ContactUsController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var contact = db.ContactUss.SingleOrDefault(c => c.id.Equals(id));
            if (contact != null)
            {
                db.ContactUss.Remove(contact);
                db.SaveChanges();
                notyfService.Success("Delete successfully");
                return RedirectToAction("Index");
            }
            else
            {
                return NoContent();
            }
        }
    }
}
