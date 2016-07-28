using ITStepTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITStepTest.Controllers
{
    public class UserController : Controller
    {
        private StoreDBEntities db = new StoreDBEntities();
        private UserService userService = new UserService();
        private MessageService messageService = new MessageService();
        //
        // GET: /User/

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;                
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            return View(userService.GetAll());
        }

        
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User selectUser = db.Users.Find(id);
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
                ViewBag.Groups = db.Groups.ToList();
            }
            if (selectUser == null)
            {
                return HttpNotFound();
            }
            return View(selectUser);
        }

        //
        // POST: /Group/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Activate(int id = 0)
        {
            userService.ActivateUserById(id, true);
            return RedirectToAction("Index");
        }

        public ActionResult Deactivate(int id = 0)
        {
            userService.ActivateUserById(id, false);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
