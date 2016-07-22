﻿using ITStepTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITStepTest.Controllers
{
    public class UserController : Controller
    {

        private StoreDBEntities db = new StoreDBEntities();
        //
        // GET: /User/

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = db.Users.FirstOrDefault(x => x.Email == userName);
                ViewBag.User = user;
                var messages = db.Messages.Count(x => x.Recipient == user.Id && x.Readed == false);
                ViewBag.Messages = messages;
            }
            return View(db.Users.ToList());
        }

    }
}