using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CheckCardNumberController : Controller
    {
        private INotyfService _notyf;
        public DatabaseContext db;
        public CheckCardNumberController(DatabaseContext _db, INotyfService notyf)
        {
            db = _db;

            _notyf = notyf;
        }

        [HttpPost]
        public IActionResult Check(string cardNumber)
        {
            var model = db.Customers.Where(c => c.card_number.Equals(cardNumber)).SingleOrDefault();
            if(model == null)
            {
                _notyf.Error("Card Number not existed");

                return RedirectToAction("Index","MainAdmin");
            }
            return RedirectToAction("Details", "Customers",new {id=model.id });

        }
    }
}
