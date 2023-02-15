using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Project.Models;

namespace Project.Controllers
{
    public class SetUpBoxController : Controller
    {
        private DatabaseContext db;
        
        public SetUpBoxController(DatabaseContext _db)
        {
            db = _db;
           
        }
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 3;
            var model = db.SetUpBoxes.AsNoTracking().OrderByDescending(s => s.id);
            PagedList<SetUpBox> sets = new PagedList<SetUpBox>(model, pageNumber, pageSize);
            ViewBag.currentPage = pageNumber;
            return View(sets);
            //var model = db.SetUpBoxes.ToList();
            //return View(model);
        }
    }
}
