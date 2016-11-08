using System;
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
    public class ResultController : Controller
    {
        private StoreDBEntities db = new StoreDBEntities();
        private UserService userService = new UserService();


        [HttpPost]
        public string UpdateRadio(int test, int question, int variantId, string update = "false")
        {
            User user = new User();
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                user = userService.GetByName(userName);                
            }
            Result result = db.Results.Where(x => x.Test == test && x.User == user.Id).FirstOrDefault();
            if (result == null)
            {
                Result newResult = new Result()
                {
                    Test = test,
                    User = user.Id,
                };

                var variantsTrue = db.Variants.Where(x=> x.Question == question && x.True && x.Id == variantId).Count();
                int testQuestions = db.Questions.Where(x => x.Test == test).Count();
                if (variantsTrue > 0)
                {
                    decimal data = 12 / testQuestions;
                    newResult.Balls = Convert.ToInt32(Math.Round(data, 2) * 100);
                }
                else {
                    newResult.Balls = 0;
                }
                db.Results.Add(newResult);
                db.SaveChanges();
            } else {
                var variantsTrue = db.Variants.Where(x => x.Question == question && x.True && x.Id == variantId).Count();
                int testQuestions = db.Questions.Where(x => x.Test == test).Count();
                if (update == "false")
                {
                    if (variantsTrue > 0)
                    {
                        decimal data = 12 / testQuestions;
                        result.Balls += Convert.ToInt32(Math.Round(data, 2) * 100);
                    }
                    else
                    {
                        result.Balls += 0;
                    }
                }
                else {
                    if (variantsTrue == 0)
                    {
                        decimal data = 12 / testQuestions;
                        result.Balls -= Convert.ToInt32(Math.Round(data, 2) * 100);
                    }                   
                }
                
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();            
            }
            return "done";
        }

        //
        // GET: /Result/

        public ActionResult Index()
        {
            return View(db.Results.ToList());
        }

        public string Information()
        {
            User user = new User();
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                user = userService.GetByName(userName);
            }
            var results = db.Results.Where(x => x.User == user.Id).ToList();
            List<TestResultModel> resultList = new List<TestResultModel>();
            foreach (var item in results) 
            {
                var test = db.Tests.Where(x => x.Id == item.Test).FirstOrDefault();
                var subject = db.Subjects.Where(x => x.Id == test.Subject).FirstOrDefault();
                resultList.Add(new TestResultModel { 
                    Test = item.Test,
                    TestName = test.Name,
                    Balls = item.Balls,
                    SubjectName = subject.Name,
                    Subject = subject.Id
                });
            }
            return JsonConvert.SerializeObject(resultList);
        }



        public string InfoUserResults(int id)
        {            
            var results = db.Results.Where(x => x.User == id).ToList();
            List<TestResultModel> resultList = new List<TestResultModel>();
            foreach (var item in results)
            {
                var test = db.Tests.Where(x => x.Id == item.Test).FirstOrDefault();
                var subject = db.Subjects.Where(x => x.Id == test.Subject).FirstOrDefault();
                resultList.Add(new TestResultModel
                {
                    Test = item.Test,
                    TestName = test.Name,
                    Balls = item.Balls,
                    SubjectName = subject.Name,
                    Subject = subject.Id
                });
            }
            return JsonConvert.SerializeObject(resultList);
        }

        //
        // GET: /Result/Details/5

        public ActionResult Details(int id = 0)
        {
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //
        // GET: /Result/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Result/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Result result)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(result);
        }

        //
        // GET: /Result/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //
        // POST: /Result/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(result);
        }

        //
        // GET: /Result/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //
        // POST: /Result/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
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