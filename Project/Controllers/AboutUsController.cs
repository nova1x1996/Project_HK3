﻿using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
	public class AboutUsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
