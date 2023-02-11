using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class PackageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
