using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class FaqController : Controller
    {
        public DatabaseContext db { get; set; }
        public FaqController(DatabaseContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var model = db.Faq.Where(f=>f.status.Equals("show")).ToList();
            return View(model);
        }
    }
}
