using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.Domain;
using System.Security.Claims;

namespace Project.Controllers
{
    public class MovieController : Controller
    {
        private DatabaseContext db;
        private UserManager<ApplicationUser> userManager;
        public MovieController(DatabaseContext _db, UserManager<ApplicationUser> _userManager)
        {
            db = _db;

        }
        public IActionResult Index()
        {
            var cate_movies = db.Movie_Cates.ToList();
           
            ViewBag.cate_movies = cate_movies;
            var model = db.Movies.Include(m => m.movie_Cate).ToList();
            return View(model);
        }

        public IActionResult Index2(int id)
        {
            var cate_movies = db.Movie_Cates.ToList();

            ViewBag.cate_movies = cate_movies;
            var model = db.Movies.Include(m => m.movie_Cate).Where(m=>m.movie_cate_id.Equals(id)).ToList();
            return View(model);
        }

    }
}
