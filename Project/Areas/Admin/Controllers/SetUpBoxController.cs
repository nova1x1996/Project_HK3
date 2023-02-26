using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Project.Models;
using System.Xml.Linq;
using System.IO;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SetUpBoxController : Controller
    {
        private DatabaseContext db;

        private readonly IWebHostEnvironment webHostEnvironment;

      

        public INotyfService notyfService { get;}
        public SetUpBoxController(DatabaseContext _db, INotyfService _notyfService, IWebHostEnvironment _webHostEnvironment)
        {
            db = _db;
            notyfService= _notyfService;
            webHostEnvironment= _webHostEnvironment;
        }
        // GET: SetUpBoxController
      
       
        public ActionResult Index()
        {
            //var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            //var pageSize = 3;
            //var model = db.SetUpBoxes.AsNoTracking().OrderByDescending(s=>s.id);
            //PagedList<SetUpBox> sets = new PagedList<SetUpBox>(model, pageNumber, pageSize);
            //ViewBag.currentPage = pageNumber;
            //return View(sets);
            var model = db.SetUpBoxes.ToList();
            return View(model);
          
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
        public ActionResult Create(SetUpBox setUpBox, string name)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    var item = db.SetUpBoxes.Where(s=>s.name.Equals(name)).FirstOrDefault();
                
                    if (item != null)
                    {
                       
                        ViewBag.msg = "This name has been created";
                    }                                     
                    else
                    {
                        string uniqueFileName = UploadFile(setUpBox);
                        var currentImage = db.SetUpBoxes.Where(i => i.img.Equals(uniqueFileName)).FirstOrDefault();
                        if (currentImage != null)
                        {
                            ViewBag.msg = "This image has been uploaded";
                        }
                        else
                        {
                            var data = new SetUpBox()
                            {
                                name = setUpBox.name,
                                details = setUpBox.details,
                                price = setUpBox.price,
                                img = uniqueFileName,
                            };

                            db.SetUpBoxes.Add(data);
                            db.SaveChanges();
                            notyfService.Success("Create new successfully");
                            return RedirectToAction("Index");
                        }
                           
                      
                                                                                               
                       

                        //if (setUpBox.imgFile.Length > 0)
                        //{
                     
                            //var filePath = Path.Combine("wwwroot/img/setupbox", setUpBox.imgFile.FileName);
                            //var stream = new FileStream(filePath, FileMode.Create);
                            //setUpBox.imgFile.CopyToAsync(stream);
                            //setUpBox.img = "/img/setupbox/" + setUpBox.imgFile.FileName;
                          

                        //}
                    }
                    
                   
                  
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"fail");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        private string UploadFile(SetUpBox setUpBox)
        {
            string uniqueFile = string.Empty;
            if (setUpBox.imgFile != null || setUpBox.imgFile.Length > 0)
            {
                string uploadFolder = Path.Combine("wwwroot" + "/img/setupbox/");
                //uniqueFile = Guid.NewGuid().ToString() + "_" + setUpBox.imgFile.FileName;
                uniqueFile = setUpBox.imgFile.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFile);
             
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        setUpBox.imgFile.CopyToAsync(stream);
                    }
                
               
             
               
              
            }
            return uniqueFile;
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
        public ActionResult Edit(SetUpBox setUpBox)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = db.SetUpBoxes.SingleOrDefault(s => s.id.Equals(setUpBox.id));
                    if (model != null)
                    {
                            var uniqueFile = string.Empty;
                            if (setUpBox.imgFile != null || setUpBox.imgFile.Length > 0)
                            {
                            //string path = Path.Combine("wwwroot/img/setupbox", setUpBox.imgFile.FileName);
                            //var stream = new FileStream(path, FileMode.Create);
                            //setUpBox.imgFile.CopyToAsync(stream);

                            //setUpBox.img = setUpBox.imgFile != null ? "/img/setupbox/" + setUpBox.imgFile.FileName : model.img;
                                if (model.img != null)
                                {
                                    string filepath = Path.Combine("wwwroot" + "/img/setupbox/", model.img);
                                    if (System.IO.File.Exists(filepath))
                                    {
                                        System.IO.File.Delete(filepath);
                                    }
                                }
                                uniqueFile = UploadFile(setUpBox);

                                model.name = setUpBox.name;
                                model.details = setUpBox.details;
                                model.price = setUpBox.price;
                                if (setUpBox.imgFile != null)
                                {
                                    model.img = uniqueFile;
                                }
                                
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
            //var setUpBox = db.SetUpBoxes.Find(id);
            if (setUpBox != null)
            {
                //delete from wwwroot/img             
                string deleteFile = Path.Combine("wwwroot" + "/img/setupbox/");
                string currentFile = Path.Combine(Directory.GetCurrentDirectory(), deleteFile, setUpBox.img);
                if (currentFile != null)
                {
                    if (System.IO.File.Exists(currentFile))
                    {
                        System.IO.File.Delete(currentFile);
                    }
                }

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
