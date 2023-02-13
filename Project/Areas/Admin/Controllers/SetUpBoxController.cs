using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SetUpBoxController : Controller
    {
        private DatabaseContext db;
        public SetUpBoxController(DatabaseContext _db)
        {
            db = _db;
        }
        // GET: SetUpBoxController
      
       
        public ActionResult Index()
        {
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
        public ActionResult Create(SetUpBox setUpBox, IFormFile file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    if (file.Length > 0)
                    {
                        var filePath = Path.Combine("wwwroot/img", file.FileName);
                        var stream = new FileStream(filePath, FileMode.Create);
                        file.CopyToAsync(stream);
                        setUpBox.img = "/img/" + file.FileName;
                        db.SetUpBoxes.Add(setUpBox);
                        db.SaveChanges();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SetUpBoxController/Edit/5
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

        // GET: SetUpBoxController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SetUpBoxController/Delete/5
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
