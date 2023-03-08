using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project.Models;
using System.ComponentModel;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class MoviesCategoryController : Controller

    {
        public INotyfService _notify { get; set; }
        private DatabaseContext db { get; set; }
        public MoviesCategoryController(DatabaseContext _db , INotyfService notify)
        {
            db = _db;
            _notify = notify;
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
            var moviecate = db.Movies.FirstOrDefault(m => m.movie_cate_id == id);
            if(moviecate != null)
            {
                _notify.Error("You cannot delete a category that contains movies");
                return RedirectToAction("Index");
            }
            var model = db.Movie_Cates.Find(id);
            

            db.Remove(model);
            db.SaveChanges();
            _notify.Success("You delete successfully !");
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
                ModelState.AddModelError(string.Empty,"The name of movie category already exists");
           
            }
            if (ModelState.IsValid)
            {
                db.Movie_Cates.Add(movie);
                db.SaveChanges();
                _notify.Success("You have successfully created");
                return RedirectToAction("Index");
            }
            return View();
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
        public ActionResult Edit(Movie_cate movive_cate)
        {
            var model = db.Movie_Cates.Where(m => m.name.Equals(movive_cate.name)).FirstOrDefault();
            if(model != null)
            {
                ModelState.AddModelError(string.Empty, "The name of movie category already exists");
            }
            if (ModelState.IsValid)
            {
                var model_2 = db.Movie_Cates.Find(movive_cate.id);
            model_2.name = movive_cate.name;


            db.SaveChanges();
                _notify.Success("You have successfully Edited");
                return RedirectToAction("Index");
            }
            return View();
     
        }

    

       
    }
}
