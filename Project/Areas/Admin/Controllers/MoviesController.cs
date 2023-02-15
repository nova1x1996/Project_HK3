using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoviesController : Controller
    {
        private DatabaseContext db { get; set; }
        private IWebHostEnvironment env { get; set; }
        
        public MoviesController(DatabaseContext _db,IWebHostEnvironment _env)
        {
            db = _db;
            env = _env;
        }
        // GET: MoviesController
        public ActionResult Index()
        {
            var model = db.Movies.Include(m => m.movie_Cate).ToList();
            return View(model);
        }


        // GET: MoviesController/Create

        public ActionResult Create()
        {
            var movies_cate = db.Movie_Cates.ToList();
            var movies_cate_list = new SelectList(movies_cate,"id","name");
            ViewBag.listcate = movies_cate_list;
            return View();
        }

        // POST: MoviesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie, IFormFile file)
        {

            try
            {


                if (ModelState.IsValid)
                {


                    if (file.Length > 0)
                    {
                        var filePath = Path.Combine("wwwroot/img/movie", file.FileName);
                        var stream = new FileStream(filePath, FileMode.Create);
                        
                        file.CopyToAsync(stream);
                        movie.img = "/img/movie/" + file.FileName;
                        db.Movies.Add(movie);
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "fail");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();

        }

        // GET: MoviesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

      

        // GET: MoviesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MoviesController/Edit/5
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

        // GET: MoviesController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = db.Movies.Find(id);
    

           
       
          
            db.Movies.Remove(model);
            db.SaveChanges();
           

         
            return RedirectToAction("Index");
        }

    }
}
