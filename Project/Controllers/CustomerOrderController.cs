using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class CustomerOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
