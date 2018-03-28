using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ABCSolutionsTest.Models;
using ABCSolutionsTest.DAL;
using Microsoft.EntityFrameworkCore;
using ABCSolutionsTest.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ABCSolutionsTest.Controllers
{
    public class HomeController : Controller
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.CurrentUser = SessionExtension.GetCurrentUser(context.HttpContext.Session);
            ViewBag.IsAuthenticated = SessionExtension.IsAuthenticated(context.HttpContext.Session);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            SessionExtension.SetCurrentUser(HttpContext.Session, null);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            ViewBag.ErrorMessage = null;

            if (ModelState.IsValid)
            {
                using (ABCTestDBConext dbctx = new ABCTestDBConext())
                {
                    try
                    {
                        dbctx.Users.Add(user);
                        dbctx.SaveChanges();
                        SessionExtension.SetCurrentUser(HttpContext.Session, user);
                        return RedirectToAction("Index", user);

                    }
                    catch (DbUpdateException x)
                    {
                        if (dbctx.Users.Count(e => e.EMail == user.EMail) > 0)
                        {
                            ViewBag.ErrorMessage = "Такой EMail уже существует!";
                        }
                        else if (dbctx.Users.Count(e => e.Login == user.Login) > 0)
                        {
                            ViewBag.ErrorMessage = "Такой Login уже существует!";
                        }
                        else
                        {
                            ViewBag.ErrorMessage = x.Message;
                        }
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Ошибочный запрос!";
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            ViewBag.ErrorMessage = null;

            if (ModelState.IsValid)
            {
                using (ABCTestDBConext dbctx = new ABCTestDBConext())
                {
                    User user = dbctx.Users.SingleOrDefault(user1 => user1.Login == model.Login);

                    if (user == null)
                    {
                        ViewBag.ErrorMessage = "Пользователь " + model.Login + " не найден!";
                    }
                    else
                    {
                        SessionExtension.SetCurrentUser(HttpContext.Session, user);
                        return RedirectToAction("Index", user);
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Ошибочный запрос!";
            }

            return View(model);
        }

    }
}
