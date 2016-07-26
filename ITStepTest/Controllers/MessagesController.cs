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
    public class MessagesController : Controller
    {
        private StoreDBEntities db = new StoreDBEntities();

        //
        // GET: /Messages/

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
            var messagesList = db.Messages.ToList().OrderByDescending(x => x.Id);
            List<MessageInformationModel> messageInfo = new List<MessageInformationModel>();
            foreach (var message in messagesList) 
            {
                var sender = db.Users.FirstOrDefault(x => x.Id == message.Sender).Email;
                var recipient = db.Users.FirstOrDefault(x => x.Id == message.Recipient).Email;
                messageInfo.Add(new MessageInformationModel { 
                    Id = message.Id,
                    Text = message.Text,
                    Sender = sender,
                    SenderId = message.Sender,
                    Recipient = recipient,
                    RecipientId = message.Recipient,
                    Readed = message.Readed
                });
            }
            return View(messageInfo);
        }

        //
        // GET: /Messages/Details/5

        public ActionResult Details(int id = 0)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        //
        // GET: /Messages/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Messages/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(message);
        }

        //
        // GET: /Messages/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        //
        // POST: /Messages/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }


        // GET: /Messages/Readed/5

        public ActionResult Readed(int id = 0)
        {
            Message message = db.Messages.Find(id);
            message.Readed = true;
            db.Entry(message).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Messages/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Messages/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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