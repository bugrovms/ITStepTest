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
    public class VariantController : Controller
    {
        private StoreDBEntities db = new StoreDBEntities();

        //
        // GET: /Variant/

        public ActionResult Index()
        {
            return View(db.Variants.ToList());
        }

        //
        // GET: /Variant/Details/5

        public ActionResult Details(int id = 0)
        {
            Variant variant = db.Variants.Find(id);
            if (variant == null)
            {
                return HttpNotFound();
            }
            return View(variant);
        }

        //
        // GET: /Variant/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Variant/Create

        [HttpPost]
        public string Create(string text, int question, string True = "false")
        {
            bool select = True == "false" ? false : true;
            Variant variant = new Variant()
            {
                Question = question,
                Text = text,
                True = select
            };
            db.Variants.Add(variant);
            db.SaveChanges();
            return "done";
        }

        [HttpPost]
        public string Edit(int Id, int question, string text, string True = "false")
        {
            bool select = True == "false" ? false : true;
            Variant variant = new Variant()
            {
                Id = Id,
                Text = text,
                True = select,
                Question = question
            };
            db.Entry(variant).State = EntityState.Modified;
            db.SaveChanges();
            return "done";
        }

        [HttpPost]
        public string Delete(int Id)
        {
            Variant variant = db.Variants.Find(Id);
            db.Variants.Remove(variant);
            db.SaveChanges();
            return "done";
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}