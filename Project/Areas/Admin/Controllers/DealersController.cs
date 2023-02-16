using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.DTO;
using Project.Repositories.Abstract;
using System.Net;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DealersController : Controller
    {
        private readonly IUserAuthenticationService _authService;
        public DatabaseContext db { get; set; }
        public DealersController(IUserAuthenticationService authService, DatabaseContext _db)
        {
            this._authService = authService;
            db = _db;
        }
        // GET: DealersController
        public ActionResult Index()
        {
            var model = db.Dealers.ToList();
            return View(model);
        }

        // GET: DealersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DealersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DealersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistrationModel model, string phone, string address)
        {
            if (!ModelState.IsValid) { return View(model); }
            model.Role = "dealer";

            var result = await this._authService.RegisterAsync(model);

            TempData["msg"] = result.Message;


            if (result.Message.Equals("You have registered successfully"))
            {
                var u1 = await db.Users.SingleOrDefaultAsync(u => u.UserName.Equals(model.Username));

                var d1 = new Dealers();
                d1.phone = phone;
                d1.address = address;
                d1.user_id = u1.Id;

                await db.Dealers.AddAsync(d1);
                await db.SaveChangesAsync();

            }
            return RedirectToAction("Index");
        }

        // GET: DealersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DealersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DealersController/Delete/5
        public async Task<IActionResult> Delete(RegistrationModel model, int id)
        {
            return View(model);
        }
    }
}
