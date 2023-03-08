using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Project.Models;
using System.Xml.Linq;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
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
        public ActionResult Create(SetUpBox setUpBox, string name, IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    ModelState.AddModelError("img", "You haven't selected an image");
                }

                if (ModelState.IsValid)
                {
                    var itemName = db.SetUpBoxes.Where(n=>n.name.Equals(name)).FirstOrDefault();
                    if (itemName != null)
                    {
                        ModelState.AddModelError("name", "This name has been created");
                    }
                    else
                    {
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        var extension = Path.GetExtension(file.FileName).ToLower();
                        if (allowedExtensions.Contains(extension) && file.Length > 0)
                        {

                            var filePath = Path.Combine("wwwroot/img/setupbox", file.FileName);
                            if (System.IO.File.Exists(filePath))
                            {
                                ModelState.AddModelError("img", "The image already exists, please choose another image !");
                                return View();
                            }
                            var stream = new FileStream(filePath, FileMode.Create);

                            file.CopyToAsync(stream);

                            setUpBox.img = "/img/setupbox/" + file.FileName;
                            db.SetUpBoxes.Add(setUpBox);
                            db.SaveChanges();
                            stream.Close();
                            notyfService.Success("Create successfully");
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "File uploaded is not an image Or you don't select file!");
                        }
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
                if (file == null)
                {
                    ModelState.Remove("file");
                    ModelState.Remove("img");
                }
                if (ModelState.IsValid)
                {
                   
                        if (file != null)
                        {

                            var checkfileExist = Path.Combine("wwwroot/img/setupbox", file.FileName);
                            if (System.IO.File.Exists(checkfileExist))
                            {
                                //ModelState.AddModelError("img", "The image already exists, please choose another image !");
                                notyfService.Error("The image already exists!");
                                //return View(setUpBox);
                                return RedirectToAction("Edit");
                            }
                            else
                            {
                                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                                var extension = Path.GetExtension(file.FileName).ToLower();
                                if (allowedExtensions.Contains(extension) && file.Length > 0)
                                {
                                    var newSetUpBox = db.SetUpBoxes.Find(setUpBox.id);
                                    // Kiểm tra nếu tệp tin đã tồn tại thì xóa tệp tin đó trước khi tải lên tệp tin mới
                                    //.TrimStart(/):Để xóa ký tự '/' đầu tiên vì Path.Combine yêu cầu các tham số ko bắt đầu bằng "/
                                    var filePathCu = Path.Combine("wwwroot", newSetUpBox.img.TrimStart('/'));
                                    if (System.IO.File.Exists(filePathCu))
                                    {
                                        System.IO.File.Delete(filePathCu);
                                    }
                                    var filePath = Path.Combine("wwwroot/img/setupbox", file.FileName);

                                    var stream = new FileStream(filePath, FileMode.Create);
                                    file.CopyToAsync(stream);
                                    newSetUpBox.name = setUpBox.name;
                                    newSetUpBox.details = setUpBox.details;
                                    newSetUpBox.price = setUpBox.price;

                                    newSetUpBox.img = "/img/setupbox/" + file.FileName;
                                    db.SaveChanges();
                                    stream.Close();
                                    notyfService.Success("Update successfully");
                                    return RedirectToAction("Index");

                                }
                            }


                        }
                        else
                        {
                            var newSetUpBox = db.SetUpBoxes.Find(setUpBox.id);
                            newSetUpBox.name = setUpBox.name;
                            newSetUpBox.details = setUpBox.details;
                            newSetUpBox.price = setUpBox.price;
                            db.SaveChanges();
                            notyfService.Success("Update successfully");
                            return RedirectToAction("Index");
                        }
                    
                    
                    //else
                    //{
                    //    ModelState.AddModelError(string.Empty, "File uploaded is not an image Or you don't select file!");
                    //}

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
            var model = db.SetUpBoxes.Find(id);
            if (model != null)
            {
                var filePath = Path.Combine("wwwroot", model.img.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                db.SetUpBoxes.Remove(model);
                db.SaveChanges();

                notyfService.Success("Delete successfully");
                return RedirectToAction("Index");
            }
            notyfService.Error("Delete Fail!");
            return RedirectToAction("Index");
        }

     
    }
}
