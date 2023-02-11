using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
