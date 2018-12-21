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
using Microsoft.EntityFrameworkCore.Infrastructure;
using StackExchange.Redis;

namespace ABCSolutionsTest.Controllers
{
    public class AdminController : Controller
    {
        private readonly ABCTestDBConext _dbctx;

        public AdminController(ABCTestDBConext dbctx)
        {
            _dbctx = dbctx;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.CurrentUser = SessionExtension.GetCurrentUser(context.HttpContext.Session);
            ViewBag.IsAuthenticated = SessionExtension.IsAuthenticated(context.HttpContext.Session);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!(ViewBag.IsAuthenticated && ViewBag.CurrentUser.IsAdmin))
                return RedirectToAction("Error", "Home");

            return View();
        }

        [HttpGet]
        public IActionResult Users()
        {
            if (!(ViewBag.IsAuthenticated && ViewBag.CurrentUser.IsAdmin))
                return RedirectToAction("Error", "Home");

            List<User> users = new List<User>();

            //using (ABCTestDBConext dbctx = new ABCTestDBConext())
            //{
                foreach(User user in _dbctx.Users)
                    users.Add(user);
            //}

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            ViewBag.ErrorMessage = null;
            ViewBag.SuccessMessage = null;

            if (ModelState.IsValid)
            {
                //using (ABCTestDBConext dbctx = new ABCTestDBConext())
                //{
                    try
                    {
                        _dbctx.Users.Add(user);
                        _dbctx.SaveChanges();
                        ViewBag.SuccessMessage = "Пользователь создан успешно!";
                    }
                    catch (Exception x)
                    {
                        if (_dbctx.Users.Count(e => e.EMail == user.EMail) > 0)
                        {
                            ViewBag.ErrorMessage = "Такой EMail уже существует!";
                        }
                        else if (_dbctx.Users.Count(e => e.Login == user.Login) > 0)
                        {
                            ViewBag.ErrorMessage = "Такой Login уже существует!";
                        }
                        else
                        {
                            ViewBag.ErrorMessage = x.Message;
                        }
                    }
                //}
            }
            else
            {
                ViewBag.ErrorMessage = "Ошибочный запрос!";
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            User user;

            //using (ABCTestDBConext _dbctx = new ABCTestDBConext())
            //{
                user = _dbctx.Users.Find(id);
            //}

            return View("Edit", user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            ViewBag.ErrorMessage = null;

            if (ModelState.IsValid)
            {
                //using (ABCTestDBConext dbctx = new ABCTestDBConext())
                //{
                    try
                    {
                        var user1 = _dbctx.Users.Find(user.Id);
                        user1.EMail = user.EMail;
                        user1.Login = user.Login;
                        user1.Name = user.Name;
                        user1.IsAdmin = user.IsAdmin;

                        _dbctx.Users.Update(user1);
                        _dbctx.SaveChanges();

                        var user2 = _dbctx.Users.Find(user.Id);

                        return RedirectToAction("Users");
                    }
                    catch (Exception x)
                    {
                        ViewBag.ErrorMessage = x.Message;
                    }
                //}
            }
            else
            {
                ViewBag.ErrorMessage = "Ошибочный запрос!";
            }

            return View("Edit", user);
        }


        [HttpGet]
        public ActionResult EditPassword(int id)
        {
            User user;

            //using (ABCTestDBConext dbctx = new ABCTestDBConext())
            //{
                user = _dbctx.Users.Find(id);
            //}

            return View("EditPassword", user);
        }

        [HttpPost]
        public ActionResult EditPassword(User user)
        {
            ViewBag.ErrorMessage = null;

            if (ModelState.IsValid)
            {
                //using (ABCTestDBConext dbctx = new ABCTestDBConext())
                //{
                    try
                    {
                        var user1 = _dbctx.Users.Find(user.Id);
                        user1.Password = user.Password;
                        _dbctx.Users.Update(user1);
                        _dbctx.SaveChanges();
                        return RedirectToAction("Users");
                    }
                    catch (Exception x)
                    {
                        ViewBag.ErrorMessage = x.Message;
                    }
                //}
            }
            else
            {
                ViewBag.ErrorMessage = "Ошибочный запрос!";
            }

            return View("EditPassword", user);
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            User user;

            //using (ABCTestDBConext dbctx = new ABCTestDBConext())
            //{
                user = _dbctx.Users.Find(id);
                _dbctx.Users.Remove(user);
                _dbctx.SaveChanges();
            //}

            return RedirectToAction("Users");
        }



    }
}
