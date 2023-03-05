using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
    public class CustomerCareController : Controller
    {
        public DatabaseContext db { get; set; }
        public CustomerCareController(DatabaseContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var location = db.Location.ToList();
            ViewBag.data = location;
            var model = db.CustomerCare.ToList();
            return View(model);
        }
        public IActionResult Index2(int Id)
        {
            var location = db.Location.ToList();

            ViewBag.data = location;
            var model = db.CustomerCare.Include(m => m.GetLocation).Where(m => m.location_id.Equals(Id)).ToList();
            return View(model);
        }
    }
}
