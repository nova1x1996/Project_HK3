using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SetUpBoxController : Controller
    {
        private DatabaseContext db;
        public INotyfService notyfService { get;}
        public SetUpBoxController(DatabaseContext _db, INotyfService _notyfService)
        {
            db = _db;
            notyfService= _notyfService;
        }
        // GET: SetUpBoxController
      
       
        public ActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 3;
            var model = db.SetUpBoxes.AsNoTracking().OrderByDescending(s=>s.id);
            PagedList<SetUpBox> sets = new PagedList<SetUpBox>(model, pageNumber, pageSize);
            ViewBag.currentPage = pageNumber;
            return View(sets);
          
        }

        // GET: SetUpBoxController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SetUpBoxController/Create
       
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SetUpBoxController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(SetUpBox setUpBox, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file.Length > 0)
                    {
                        var filePath = Path.Combine("wwwroot/img", file.FileName);
                        var stream = new FileStream(filePath, FileMode.Create);
                        file.CopyToAsync(stream);
                        setUpBox.img = "/img/" + file.FileName;
                        db.SetUpBoxes.Add(setUpBox);
                        db.SaveChanges();
                        notyfService.Success("Create new successfully");
                        return RedirectToAction("Index");

                    }
                  
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

        // GET: SetUpBoxController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            SetUpBox setUpBox = db.SetUpBoxes.Find(id);
            return View(setUpBox);
        }

        // POST: SetUpBoxController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(SetUpBox setUpBox, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = db.SetUpBoxes.SingleOrDefault(s => s.id.Equals(setUpBox.id));
                    if (model != null)
                    {
                        if (file != null || file.Length > 0)
                        {
                            string path = Path.Combine("wwwroot/img", file.FileName);
                            var stream = new FileStream(path, FileMode.Create);
                            file.CopyToAsync(stream);

                            setUpBox.img = file != null ? "/img/" + file.FileName : model.img;
                            model.name = setUpBox.name;
                            model.details = setUpBox.details;
                            model.price = setUpBox.price;
                            model.img = setUpBox.img;
                            db.SaveChanges();
                            notyfService.Success("Update successfully");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            model.name = setUpBox.name;
                            model.details = setUpBox.details;
                            model.price = setUpBox.price;
                           
                            db.SaveChanges();
                            notyfService.Success("Update successfully");
                            return RedirectToAction("Index");
                        }
                       
                    }
                    else
                    {
                        return NoContent();
                    }
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

        // GET: SetUpBoxController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var setUpBox = db.SetUpBoxes.SingleOrDefault(b => b.id.Equals(id));
            if (setUpBox != null)
            {
                db.SetUpBoxes.Remove(setUpBox);
                db.SaveChanges();
                notyfService.Success("Delete successfully");
                return RedirectToAction("Index");
            }
            else
            {
                return NoContent();
            }
        }

        // POST: SetUpBoxController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    var order = db.Orders.SingleOrDefault(b => b.OrderID.Equals(id));
        //    if (order != null)
        //    {
        //        db.Orders.Remove(order);
        //        db.SaveChanges();
        //        return RedirectToAction("Index", "Order");
        //    }
        //    else
        //    {
        //        return NoContent();
        //    }
        //}
    }
}
