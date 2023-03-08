using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,dealer")]
    public class MainAdminController : Controller
    {
        private DatabaseContext db;
        public MainAdminController(DatabaseContext _db)
        {
            db = _db;
        }
        // GET: MainAdminController
        public ActionResult Index()
        {


            //Chart Tổng Tiền
            decimal? TongTien = 0;
            var OrderCustomer = db.Customer_orders.Where(c => c.state == true).ToList();
            foreach(var item in OrderCustomer)
            {
                TongTien = TongTien + item.total_money;
            }
            var RechargeOrder = db.Recharges.Where(r => r.state == true).Include(r=>r.GetPackage).ToList();
            foreach (var item in RechargeOrder)
            {
                TongTien = TongTien + (item.GetPackage.price * item.month);
            }

            var CPOrder = db.ChangePackages.Where(r => r.state == true).ToList();
            foreach (var item in CPOrder)
            {
                TongTien = TongTien + item.price;
            }

          
            ViewBag.TongTien = TongTien;

            //Chart Tổng Tiền



            int TongDonHang = OrderCustomer.Count() + RechargeOrder.Count() + CPOrder.Count();
            ViewBag.TongDonHang = TongDonHang;
            var Customer = db.Customers.ToList();
            ViewBag.TongNguoi = Customer;

            ViewBag.TongFB = db.Feed_Backs.Count();
            return View();
        }

        // GET: MainAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MainAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MainAdminController/Create
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

        // GET: MainAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MainAdminController/Edit/5
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

        // GET: MainAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MainAdminController/Delete/5
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
