﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PackagesController : Controller
    {
        // GET: PackagesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PackagesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PackagesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PackagesController/Create
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

        // GET: PackagesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PackagesController/Edit/5
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

        // GET: PackagesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PackagesController/Delete/5
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