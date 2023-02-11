using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class FaqController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
