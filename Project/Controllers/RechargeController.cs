using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
    public class RechargeController : Controller
    {
        private DatabaseContext db;
        public RechargeController(DatabaseContext _db)
        {
            db = _db;
        }
        
        public IActionResult Index(string? id)
        {
          
            var model = db.Customers.Include(c=>c.package).SingleOrDefault(r=>r.user_id.Equals(id));
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string customerId,string? s)
        {
            var model = db.Customers.Include(c=>c.package).SingleOrDefault(c=>c.id==int.Parse(customerId));
            
            if(model != null)
            {
                return View(model);
            }
            
            return View();
        }
    }
}
