using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoviesCategoryController : Controller

    {
        private DatabaseContext db { get; set; }
        public MoviesCategoryController(DatabaseContext _db)
        {
            db = _db;
        }
        // GET: MoviesCategoryController
        public ActionResult Index()
        {
            var model = db.Movie_Cates.ToList();
            return View(model);
        }

        // GET: MoviesCategoryController/Delete/5
        [HttpGet()]
        public ActionResult Delete(int id)
        {
            var model = db.Movie_Cates.Find(id);
            db.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: MoviesCategoryController/Create
        [HttpGet()]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie_cate movie)
        {
            var model = db.Movie_Cates.Where(m => m.name.Equals(movie.name)).SingleOrDefault();
            if(model != null)
            {
                return Content("Đã tồn tại ");
            }
            db.Movie_Cates.Add(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
     

        // GET: MoviesCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = db.Movie_Cates.Find(id);
            
            return View(model);
        }

        // POST: MoviesCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,string name)
        {
            var model = db.Movie_Cates.Where(m => m.name.Equals(name)).SingleOrDefault();
            if(model != null)
            {
                return Content("Đã tồn tại ");
            }
            var model_2 = db.Movie_Cates.Find(id);
            model_2.name = name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    

       
    }
}
