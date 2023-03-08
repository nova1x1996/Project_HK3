using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class MoviesBookingController : Controller
    {
        // GET: MoviesBookingController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MoviesBookingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MoviesBookingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MoviesBookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: MoviesBookingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MoviesBookingController/Edit/5
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

        // GET: MoviesBookingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MoviesBookingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
