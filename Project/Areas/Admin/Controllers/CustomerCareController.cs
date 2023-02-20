using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Controllers;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    public class CustomerCareController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
