using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Project.Models;
using System.Security.Claims;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "dealer")]
    public class DealersOrderController : Controller
	{
		public INotyfService notyfService { get; }
		public DatabaseContext db { get; set; }
        public DealersOrderController(DatabaseContext _db, INotyfService _notyfService)
        {
            db = _db;
			notyfService = _notyfService;
		}
        // Cho Dealer
        [Authorize(Roles = "dealer")]
        public ActionResult Index()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = db.Dealer_Orders
                .Include(d => d.GetDealer)
                .ThenInclude(d => d.ApplicationUser)
                .Include(d => d.GetSetUpBox)
                .Where(d => d.GetDealer.user_id == userId)
                .ToList();

            return View(model);
        }



        // Cho ADMIN
        [Authorize(Roles = "dealer")]
        public ActionResult Index2()
        {
            var model = db.Dealer_Orders
                .Include(d => d.GetDealer)
                .ThenInclude(d => d.ApplicationUser)
                .Include(d => d.GetSetUpBox)
                .ToList();

            return View(model);
        }


        // GET: DealersOrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

		// GET: DealersOrderController/Create
		[Authorize(Roles = "dealer")]
		public ActionResult Create()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Find the current dealer based on their user ID
            var dealer = db.Dealers.SingleOrDefault(d => d.user_id == userId);

            if (dealer == null)
            {
                return NotFound();
            }
            ViewBag.data = new SelectList(db.SetUpBoxes.ToList(), "id", "name");
            return View();
        }
        // POST: DealersOrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DealersOrder newDealersOrder)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //Tìm Dealers dựa trên ID của Phiên Đăng nhập hiện tại
            var dealers = db.Dealers.Where(d => d.user_id.Equals(userId)).SingleOrDefault();
            newDealersOrder.dealers_id = dealers.id;
            ViewBag.data = new SelectList(db.SetUpBoxes.ToList(), "id", "name");
            try
            {
                if (ModelState.IsValid)
                {
                    if(newDealersOrder.date.Day <= DateTime.Now.Day)
                    {
                        notyfService.Error("Order Date must be greater than today");
                        return View();
                    }
                    
                    db.Dealer_Orders.Add(newDealersOrder);
                    db.SaveChanges();
					notyfService.Success("Create new successfully");
					return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "New creation failed.\r\nPlease enter full information.");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        // GET: DealersOrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DealersOrderController/Edit/5
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

        // GET: DealersOrderController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var model = db.Dealer_Orders.SingleOrDefault(m => m.id.Equals(id));
                if (model != null)
                {
                    db.Dealer_Orders.Remove(model);
                    db.SaveChanges();
					notyfService.Success("Delete successfully");
					return RedirectToAction("Index");
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
