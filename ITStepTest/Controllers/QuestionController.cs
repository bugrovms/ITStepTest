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
    public class QuestionController : Controller
    {
        private StoreDBEntities db = new StoreDBEntities();
        private UserService userService = new UserService();
        private MessageService messageService = new MessageService();

        //
        // GET: /Question/

        public ActionResult Index(int id = 0)
        {

            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            ViewBag.Test = db.Tests.Find(id);
            var questions = db.Questions.Where(x => x.Test == id).ToList();
            List<QuestionInformationModel> result = new List<QuestionInformationModel>();
            foreach (var question in questions)
            {
                var variants = db.Variants.Where(x => x.Question == question.Id).ToList();
                result.Add(new QuestionInformationModel
                {
                    Id = question.Id,
                    Test = question.Test,
                    Text = question.Text,
                    Variants = variants
                });

            }
            return View(result);
        }

        public ActionResult List(int id = 0)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            ViewBag.Test = db.Tests.Find(id);
            var questions = db.Questions.Where(x => x.Test == id).ToList();
            List<QuestionInformationModel> result = new List<QuestionInformationModel>();
            foreach (var question in questions)
            {
                var variants = db.Variants.Where(x => x.Question == question.Id).ToList();
                result.Add(new QuestionInformationModel
                {
                    Id = question.Id,
                    Test = question.Test,
                    Text = question.Text,
                    Variants = variants
                });

            }
            return View(result);
        }

        //
        // GET: /Question/Details/5

        public ActionResult Details(int id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // GET: /Question/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Question/Create

        [HttpPost]
        public string Create(string text, int test)
        {
            Question question = new Question()
            {
                Test = test,
                Text = text
            };
            db.Questions.Add(question);
            db.SaveChanges();
            return "done";           
        }

        //
        // GET: /Question/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            ViewBag.TestId = question.Test;
            return View(question);
        }

        //
        // POST: /Question/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List", new { id = question.Test });
            }
            return View(question);
        }

        //
        // GET: /Question/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            ViewBag.TestId = question.Test;
            return View(question);
        }

        //
        // POST: /Question/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("List", new { id = question.Test });
        }

        [HttpPost]
        public ActionResult CreateModal(int questionId, int testId)
        {
            ViewBag.QuestionId = questionId;
            ViewBag.TestId = testId;
            return PartialView("ModalPartial");
        }

        [HttpPost]
        public ActionResult DeleteVariantModal(int Id, string link)
        {
            Variant variant = db.Variants.Find(Id);
            ViewBag.Variant = variant;
            ViewBag.Link = link;
            return PartialView("DeleteVariantModalPartial");
        }

        [HttpPost]
        public ActionResult EditVariantModal(int Id, string link)
        {
            Variant variant = db.Variants.Find(Id);
            ViewBag.Variant = variant;
            ViewBag.Link = link;
            return PartialView("EditVariantModalPartial");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}