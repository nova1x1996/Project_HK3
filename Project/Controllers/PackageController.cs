using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using Project.Models;
using System.Security.Claims;

namespace Project.Controllers
{
    public class PackageController : Controller
    {
        public DatabaseContext db { get; set; }
        public PackageController(DatabaseContext _db)
        {
            db = _db;
        }
       
        public IActionResult Index()
        {
            var model = db.Packages.ToList();
            return View(model);
        }

       

   
    }
}
