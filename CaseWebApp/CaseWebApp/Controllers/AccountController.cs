using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseWebApp.Models;
using CaseWebApp.Models.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CaseWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly DataContext db;

        public AccountController(ILogger<AccountController> logger,DataContext _db)
        {
            _logger = logger;
            db = _db;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Users userData)
        {
            var user= db.Users.Where(m => m.UserName== userData.UserName && m.Password== userData.Password && m.IsActive == true).FirstOrDefault();

            if (user != null)
            {

                HttpContext.Session.SetString(Globals.Session_UserName, user.UserName);
                HttpContext.Session.SetString(Globals.Session_UserId,Convert.ToString(user.Id));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Mesaj = "Username or password is wrong!";
                return View();
            }
        }

    }
}
