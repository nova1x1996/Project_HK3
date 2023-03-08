using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
  
    public class AccountController : Controller
    {
        private readonly INotyfService _notyf;

        public AccountController(INotyfService notyf)
        {
            _notyf = notyf;
        }
        public IActionResult Index()
        {
            string url = Request.Headers["Referer"].ToString();

            if (url != null && url != "")
            {
                // Redirect về trang trước đó
                _notyf.Error("You do not have permission to access this page !");
                return Redirect(url);
            }
      
            else 
            {
                // Nếu không có trang trước đó thì redirect về trang chủ
                _notyf.Error("You do not have permission to access this page !");
                return RedirectToAction("Index", "Home");
            }

            _notyf.Error("Fail !");
            return RedirectToAction("Index","Home");
        }
    }
}
