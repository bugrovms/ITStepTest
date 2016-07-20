using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITStepTest.Models;
using System.Web.Security;

namespace ITStepTest.Controllers
{
    public class HomeController : Controller
    {

        private StoreDBEntities db = new StoreDBEntities();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) {
                var userName = User.Identity.Name;
                var user = db.Users.FirstOrDefault(x => x.Email == userName);
                ViewBag.User = user;
            }            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
