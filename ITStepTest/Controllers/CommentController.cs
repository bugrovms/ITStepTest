﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITStepTest.Models;
using Newtonsoft.Json;

namespace ITStepTest.Controllers
{
    public class CommentController : Controller
    {
        private StoreDBEntities db = new StoreDBEntities();
        private UserService userService = new UserService();
        private MessageService messageService = new MessageService();
        //
        // GET: /Comment/

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            var comments = db.Comments.OrderByDescending(x => x.Date).ToList();
            List<CommentInformationModel> commentsList = new List<CommentInformationModel>();
            foreach(var comment in comments)
            {
                var user = db.Users.Find(comment.User);
                var test = db.Tests.Find(comment.Test);
                var subject = db.Subjects.Find(test.Subject);

                commentsList.Add(new CommentInformationModel { 
                    Id=comment.Id,
                    Text = comment.Text,
                    User = comment.User,
                    Role = user.Role,
                    UserFullName = user.LastName + " " + user.FirstName,
                    Test = comment.Test,
                    TestName = test.Name,
                    Subject = test.Subject,
                    SubjectName = subject.Name,
                    Date = comment.Date
                });
            }
            return View(commentsList);
        }

        //
        // GET: /Comment/Details/5

        public ActionResult Details(int id = 0)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // GET: /Comment/Create

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            ViewBag.Tests = db.Tests.ToList();
            return View();
        }

        //
        // POST: /Comment/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comment);
        }

        //
        // POST: /Comment/UserCreate

        [HttpPost]
        public string UserCreate(string text, string test)
        {
            try {
                if (ModelState.IsValid)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        int testId = Convert.ToInt32(test);
                        var userName = User.Identity.Name;
                        var user = userService.GetByName(userName);
                        Comment commentNew = new Comment
                        {
                            Text = text,
                            User = user.Id,
                            Test = testId
                        };
                        db.Comments.Add(commentNew);
                        db.SaveChanges();
                        var comments = db.Comments.Where(x => x.Test == testId).OrderByDescending(x => x.Date).ToList();
                        List<CommentInformationModel> commentsList = new List<CommentInformationModel>();
                        foreach (var comment in comments)
                        {
                            var userC = db.Users.Find(comment.User);
                            var testC = db.Tests.Find(comment.Test);
                            var subject = db.Subjects.Find(testC.Subject);

                            commentsList.Add(new CommentInformationModel
                            {
                                Id = comment.Id,
                                Text = comment.Text,
                                User = comment.User,
                                Role = userC.Role,
                                UserFullName = userC.LastName + " " + userC.FirstName,
                                Test = comment.Test,
                                TestName = testC.Name,
                                Subject = testC.Subject,
                                SubjectName = subject.Name,
                                Date = comment.Date
                            });
                        }
                        return JsonConvert.SerializeObject(commentsList);
                    }                    
                }
                return "error";
            }
            catch (Exception ex)
            {
                return "error";
            }            
        }

        //
        // GET: /Comment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Comment comment = db.Comments.Find(id);
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // POST: /Comment/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        //
        // GET: /Comment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Comment comment = db.Comments.Find(id);
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // POST: /Comment/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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