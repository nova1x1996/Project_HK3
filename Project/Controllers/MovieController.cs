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

        [HttpGet()]
        public IActionResult MovieOrder(int id)
        {
            var model = db.Movies.Find(id);

            return View(model);
        }

        [HttpPost()]
        public IActionResult MovieOrder(string pay_type,string total_money,int movies_id)
        {
            //Lấy Id của Phiên Đăng nhập hiện tại
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Tìm Customer dựa trên ID của Phiên Đăng nhập hiện tại
            var customer = db.Customers.Where(c => c.user_id.Equals(userId)).SingleOrDefault();

            var Mo = new Customer_order();
            Mo.total_money = decimal.Parse(total_money);
            Mo.pay_type = pay_type;
            Mo.customer_id = customer.id;
            Mo.movie_id = movies_id;
            db.Customer_orders.Add(Mo);
            db.SaveChanges();




            TempData["thongBao"] = "You have successfully placed an order.";
            return RedirectToAction("Index","Movie");


            if (!ModelState.IsValid)
            {
                string a = "";
                foreach (var item in ModelState.Values)
                {
                    a = a + item.Errors.ToString();
                }
                return Content(a);
            }
        }
    }
}
