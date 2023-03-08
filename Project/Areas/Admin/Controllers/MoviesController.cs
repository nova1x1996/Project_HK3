using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class MoviesController : Controller
    {
        public INotyfService _notify { get; set; }
        private DatabaseContext db { get; set; }
        private IWebHostEnvironment env { get; set; }

        public MoviesController(DatabaseContext _db, IWebHostEnvironment _env, INotyfService notify)
        {
            db = _db;
            env = _env;
            _notify = notify;
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
            var movies_cate_list = new SelectList(movies_cate, "id", "name");
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
                var movies_cate = db.Movie_Cates.ToList();
                var movies_cate_list = new SelectList(movies_cate, "id", "name");
                ViewBag.listcate = movies_cate_list;
                if (file == null)
                {
                    ModelState.AddModelError("img", "You haven't selected an image");
                }

                if (ModelState.IsValid)
                {


                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(file.FileName).ToLower();
                    if (allowedExtensions.Contains(extension) && file.Length > 0)
                    {

                        var filePath = Path.Combine("wwwroot/img/movie", file.FileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            ModelState.AddModelError("img", "The image already exists, please choose another image !");
                            return View();
                        }
                        var stream = new FileStream(filePath, FileMode.Create);

                        file.CopyToAsync(stream);

                        movie.img = "/img/movie/" + file.FileName;
                        db.Movies.Add(movie);
                        db.SaveChanges();
                        stream.Close(); _notify.Success("You have successfully created");
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "File uploaded is not an image Or you don't select file!");
                    }



                }

                return View();


            }
            catch(Exception ex )
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
            var model = db.Movies.Find(id);
            var movies_cate = db.Movie_Cates.ToList();
            var movies_cate_list = new SelectList(movies_cate, "id", "name");
            ViewBag.listcate = movies_cate_list;
            return View(model);
        }

        // POST: MoviesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie, IFormFile file)
        {
            var movies_cate = db.Movie_Cates.ToList();
            var movies_cate_list = new SelectList(movies_cate, "id", "name");
            ViewBag.listcate = movies_cate_list;
            if (file == null)
            {
                ModelState.Remove("file");
                ModelState.Remove("img");
            }

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(file.FileName).ToLower();
                    if (allowedExtensions.Contains(extension) && file.Length > 0)
                    {
                        var movieMoi = db.Movies.Find(movie.id);
                        // Kiểm tra nếu tệp tin đã tồn tại thì xóa tệp tin đó trước khi tải lên tệp tin mới
                        //.TrimStart(/):Để xóa ký tự '/' đầu tiên vì Path.Combine yêu cầu các tham số ko bắt đầu bằng "/
                        var filePathCu = Path.Combine("wwwroot", movieMoi.img.TrimStart('/'));
                        if (System.IO.File.Exists(filePathCu))
                        {
                            System.IO.File.Delete(filePathCu);
                        }
                        var filePath = Path.Combine("wwwroot/img/movie", file.FileName);
                        var stream = new FileStream(filePath, FileMode.Create);
                        file.CopyToAsync(stream);
                        movieMoi.name = movie.name;
                        movieMoi.movie_cate_id = movie.movie_cate_id;
                        movieMoi.price = movie.price;
                        movieMoi.content = movie.content;
                        movieMoi.img = "/img/movie/" + file.FileName;


                        db.SaveChanges();
                        stream.Close();
                        _notify.Success("You have successfully Edit");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "File uploaded is not an image !");
                    }
                }
                else
                {
                    var movieMoi = db.Movies.Find(movie.id);
                    movieMoi.name = movie.name;
                    movieMoi.movie_cate_id = movie.movie_cate_id;
                    movieMoi.price = movie.price;
                    movieMoi.content = movie.content;
                    db.SaveChanges();
                    _notify.Success("You have successfully Edit");
                    return RedirectToAction("Index");
                }



            }
          

            return View();
        }

        // GET: MoviesController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {

            var model = db.Movies.Find(id);
            if (model != null)
            {
                var filePath = Path.Combine("wwwroot", model.img.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                db.Movies.Remove(model);
                db.SaveChanges();

                _notify.Success("You delete successfully !");
                return RedirectToAction("Index");
            }
            _notify.Error("You can't delete !");
            return RedirectToAction("Index");


        }

    }
}
