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
    public class DealersOrderController : Controller
    {
        public DatabaseContext db { get; set; }
        public DealersOrderController(DatabaseContext _db)
        {
            db = _db;
        }
        // GET: DealersOrderController
        public ActionResult Index()
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
        public ActionResult Create()
        {
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
                    
                    db.Dealer_Orders.Add(newDealersOrder);
                    db.SaveChanges();
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
