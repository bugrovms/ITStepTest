using ITStepTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITStepTest.Controllers
{
    public class UserController : Controller
    {

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

    }
}
