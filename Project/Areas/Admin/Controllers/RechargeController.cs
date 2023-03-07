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
    public class RechargeController : Controller
    {
        private DatabaseContext db;
        public INotyfService notyfService { get; }
        public RechargeController(DatabaseContext _db, INotyfService _notyfService)
        {
            db = _db;
            notyfService = _notyfService;
        }
        // GET: RechargeController
        public ActionResult Index()
        {
            var model = db.Recharges.Include(r => r.GetCustomer)
                .ThenInclude(r => r.ApplicationUser)
                .Include(r => r.GetPackage).ToList();

            return View(model);
        }

        public ActionResult ConfirmPayment(int id)
        {
            var recharge = db.Recharges.Find(id);
            var customer = db.Customers.Find(recharge.customer_id);

            DateTime ThoiGianConLai = (DateTime)(customer.date_left);
            customer.date_left = ThoiGianConLai.AddMonths(recharge.month);
            recharge.state = true;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        // GET: RechargeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RechargeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RechargeController/Create
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

        // GET: RechargeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RechargeController/Edit/5
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

        // GET: RechargeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RechargeController/Delete/5
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
