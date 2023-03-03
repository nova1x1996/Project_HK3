using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Project.Models;
using Project.Services;
using System.Diagnostics;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext db { set; get; }
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DatabaseContext _db)
        {
            _logger = logger;
            db = _db;
        }

     
        public IActionResult Index()
        {
            var movies = db.Movies.ToList();

            //ViewBag.cate_movies = cate_movies;
            //var model = db.Movies.Include(m => m.movie_Cate).ToList();
            //return View(movies);

            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}