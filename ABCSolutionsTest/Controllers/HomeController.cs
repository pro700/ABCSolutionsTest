using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ABCSolutionsTest.Models;

namespace ABCSolutionsTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("CurrentUser", "Sokol");
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = HttpContext.Session.GetString("CurrentUser");

            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Register()
        {
            ViewData["Message"] = "";

            return View();
        }

        public IActionResult Login()
        {
            ViewData["Message"] = "";

            return View();
        }

        public IActionResult Logout()
        {
            ViewData["Message"] = "";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
