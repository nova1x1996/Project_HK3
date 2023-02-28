using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Controllers;
using Project.Models;
using Project.Models.Domain;
using Project.Models.DTO;
using Project.Repositories.Abstract;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerCareController : Controller
    {
        public INotyfService notyfService { get; }
        public DatabaseContext db { get; set; }
        public CustomerCareController(DatabaseContext _db, INotyfService _notyfService)
        {
            db = _db;
            notyfService = _notyfService;
        }
        public IActionResult Index()
        {
            var model = db.CustomerCare
               .Include(d => d.GetLocation)
               .ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            var location = db.Location.ToList();
            var locations = new SelectList(location, "Id", "Name");
            ViewBag.data = locations;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCare newCustomerCare)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CustomerCare.Add(newCustomerCare);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ModelState.ErrorCount.ToString());
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var deal = db.CustomerCare.SingleOrDefault(d => d.Id.Equals(id));
            return View(deal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerCare customercare)
        {
            try
            {
                var pac = db.CustomerCare.SingleOrDefault(e => e.Id.Equals(customercare.Id));
                if (pac != null && ModelState.IsValid)
                {
                    pac.Id = customercare.Id;
                    db.SaveChanges();
                    notyfService.Success("Edit successfully");
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

        public ActionResult Delete(int id)
        {
            try
            {
                var model = db.CustomerCare.SingleOrDefault(m => m.Id.Equals(id));
                if (model != null)
                {
                    db.CustomerCare.Remove(model);
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
