using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fatec.AAP4.Web.Models;

namespace Fatec.AAP4.Web.Controllers
{
    public class DreController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: Dre
        public ActionResult Index()
        {
            return View(db.dre.ToList());
        }

        // GET: Dre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dre dre = db.dre.Find(id);
            if (dre == null)
            {
                return HttpNotFound();
            }
            return View(dre);
        }

        // GET: Dre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_dre,nome_dre")] dre dre)
        {
            if (ModelState.IsValid)
            {
                db.dre.Add(dre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dre);
        }

        // GET: Dre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dre dre = db.dre.Find(id);
            if (dre == null)
            {
                return HttpNotFound();
            }
            return View(dre);
        }

        // POST: Dre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_dre,nome_dre")] dre dre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dre);
        }

        // GET: Dre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dre dre = db.dre.Find(id);
            if (dre == null)
            {
                return HttpNotFound();
            }
            return View(dre);
        }

        // POST: Dre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dre dre = db.dre.Find(id);
            db.dre.Remove(dre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
