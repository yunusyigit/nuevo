using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CaseWebApp.Models;
using Microsoft.AspNetCore.Http;

namespace CaseWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(Globals.Session_UserId)))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.UserName = HttpContext.Session.GetString(Globals.Session_UserName);
                return View();
            }

        }

        public IActionResult ErorList()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(Globals.Session_UserId)))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.UserName = HttpContext.Session.GetString(Globals.Session_UserName);
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
