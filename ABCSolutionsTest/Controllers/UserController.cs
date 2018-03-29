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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ABCSolutionsTest.Controllers
{
    public class UserController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.CurrentUser = SessionExtension.GetCurrentUser(context.HttpContext.Session);
            ViewBag.IsAuthenticated = SessionExtension.IsAuthenticated(context.HttpContext.Session);
        }


        [HttpGet]
        public IActionResult CreateMessage()
        {
            Message message = new Message();
            message.Time = DateTime.Now;

            FillUsers();

            return View(message);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMessage(Message message)
        {
            ViewBag.ErrorMessage = null;
            ViewBag.SuccessMessage = null;

            if (ModelState.IsValid)
            {
                using (ABCTestDBConext dbctx = new ABCTestDBConext())
                {
                    try
                    {
                        message.Time = DateTime.Now;
                        message.AuthorID = ViewBag.CurrentUser.Id;
                        dbctx.Messages.Add(message);
                        dbctx.SaveChanges();
                        ViewBag.SuccessMessage = "Сообщение успешно отправлено!";
                    }
                    catch (Exception x)
                    {
                        ViewBag.ErrorMessage = x.Message;
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Ошибочный запрос!";
            }

            FillUsers();

            return View(message);
        }


        [HttpGet]
        public IActionResult Messages()
        {
            List<Message> messages = new List<Message>();

            using (ABCTestDBConext db = new ABCTestDBConext())
            {
                int CurrentUserId = ViewBag.CurrentUser.Id;

                messages = db.Messages
                    .Where(e => e.UserID == CurrentUserId)
                    .Include(e => e.Author)
                    .ToList();

            }

            return View(messages);

        }


        private void FillUsers()
        {
            List<User> users = new List<User>();

            using (ABCTestDBConext dbctx = new ABCTestDBConext())
            {
                users.AddRange(dbctx.Users);
            }

            ViewBag.Users = new SelectList(users, "Id", "Name");
        }
    }
}