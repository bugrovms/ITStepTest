﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITStepTest.Models;

namespace ITStepTest.Controllers
{
    public class SubjectController : Controller
    {
        private StoreDBEntities db = new StoreDBEntities();
        private UserService userService = new UserService();
        private MessageService messageService = new MessageService();

        //
        // GET: /Subject/

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }    
            return View(db.Subjects.ToList());
        }

        //
        // GET: /Subject/Details/5

        public ActionResult Details(int id = 0)
        {
            Subject subject = db.Subjects.Find(id);
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }    
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        //
        // GET: /Subject/Create

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }    
            return View();
        }

        //
        // POST: /Subject/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subject);
        }

        //
        // GET: /Subject/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Subject subject = db.Subjects.Find(id);
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }    
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        //
        // POST: /Subject/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        //
        // GET: /Subject/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Subject subject = db.Subjects.Find(id);
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }    
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        //
        // POST: /Subject/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}