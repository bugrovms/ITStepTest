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
        // POST: /User/Edit/5

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

        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            User userSelect = db.Users.Find(id);
            Group groupSelect = db.Groups.Find(userSelect.GroupId);
            var selectRole = "";
            switch (userSelect.Role) { 
                case 0:
                    selectRole = "Студент";
                    break;
                case 1:
                    selectRole = "Преподаватель";
                    break;
                case 2:
                    selectRole = "Администратор";
                    break;
            }
            UserInformationModel userInfo = new UserInformationModel
            {
                Id=userSelect.Id,
                Email=userSelect.Email,
                LastName=userSelect.LastName,
                FirstName=userSelect.FirstName,
                Date=userSelect.Date,
                DateCreate=userSelect.DateCreate,
                Active=userSelect.Active,
                Phone=userSelect.Phone,                
                Role = selectRole,
                RoleId = userSelect.Role
            };
            if (groupSelect == null)
            {
                userInfo.Group = "Группа не задана";
            }
            else {
                userInfo.Group = groupSelect.Name;
            }
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
                ViewBag.Groups = db.Groups.ToList();
            }
            if (userSelect == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User userSelect = db.Users.Find(id);
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = userService.GetByName(userName);
                ViewBag.User = user;
                ViewBag.Messages = messageService.GetRecepientNotReadCount(user.Id);
            }
            if (userSelect == null)
            {
                return HttpNotFound();
            }
            return View(userSelect);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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
