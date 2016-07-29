using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITStepTest.Models;

namespace ITStepTest.Controllers
{
    public class TestController : Controller
    {
        private StoreDBEntities db = new StoreDBEntities();
        private UserService userService = new UserService();
        private MessageService messageService = new MessageService();

        //
        // GET: /Test/

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            } 
            return View(db.Tests.ToList());
        }

        // GET: /Test/:SubjectId
        public ActionResult Tests(int id=0)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            var subject = db.Subjects.FirstOrDefault(x=> x.Id == id);
            if (subject != null)
            {
                ViewBag.Subject = subject;
            }           

            return View(db.Tests.Where(x=>x.Subject == id).ToList());
        }

        //
        // GET: /Test/Details/5

        public ActionResult Details(int id = 0)
        {
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        //
        // GET: /Test/Create

        public ActionResult Create(int id=0, string page = "Index")
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            var subject = db.Subjects.FirstOrDefault(x => x.Id == id);
            if (subject != null)
            {
                ViewBag.Subject = subject;
            }
            ViewBag.Page = page;
            return View();
        }

        //
        // POST: /Test/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TestInformationModel test)
        {
            var newTest = new Test
            {
                Id = test.Id,
                Name = test.Name,
                Subject = test.Subject
            };
            if (ModelState.IsValid)
            {
                db.Tests.Add(newTest);
                db.SaveChanges();
                return RedirectToAction(test.Page, new { id = test.Subject });
            }

            return View(test);
        }

        //
        // GET: /Test/Edit/5

        public ActionResult Edit(int id = 0, string page = "Index")
        {
            Test test = db.Tests.Find(id);
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            if (test == null)
            {
                return HttpNotFound();
            }
            var newTest = new TestInformationModel
            {
                Id = test.Id,
                Name = test.Name,
                Subject = test.Subject,
                Page = page
            };
            ViewBag.Page = page;
            return View(newTest);
        }

        //
        // POST: /Test/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TestInformationModel test)
        {
            if (ModelState.IsValid)
            {
                var newTest = new Test
                {
                    Id = test.Id,
                    Name = test.Name,
                    Subject = test.Subject
                };
                db.Entry(newTest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(test.Page, new { id = test.Subject });
            }
            return View(test);
        }

        //
        // GET: /Test/Delete/5

        public ActionResult Delete(int id = 0, string page = "Index")
        {
            Test test = db.Tests.Find(id);
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            if (test == null)
            {
                return HttpNotFound();
            }
            var newTest = new TestInformationModel
            {
                Id = test.Id,
                Name = test.Name,
                Subject = test.Subject,
                Page = page
            };
            ViewBag.Page = page;
            return View(newTest);
        }

        //
        // POST: /Test/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(TestInformationModel data)
        {
            Test test = db.Tests.Find(data.Id);
            db.Tests.Remove(test);
            db.SaveChanges();
            return RedirectToAction(data.Page, new { id = test.Subject });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}