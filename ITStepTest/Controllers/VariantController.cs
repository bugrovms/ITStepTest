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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Variant variant)
        {
            if (ModelState.IsValid)
            {
                db.Variants.Add(variant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(variant);
        }

        //
        // GET: /Variant/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Variant variant = db.Variants.Find(id);
            if (variant == null)
            {
                return HttpNotFound();
            }
            return View(variant);
        }

        //
        // POST: /Variant/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Variant variant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(variant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(variant);
        }

        //
        // GET: /Variant/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Variant variant = db.Variants.Find(id);
            if (variant == null)
            {
                return HttpNotFound();
            }
            return View(variant);
        }

        //
        // POST: /Variant/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Variant variant = db.Variants.Find(id);
            db.Variants.Remove(variant);
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