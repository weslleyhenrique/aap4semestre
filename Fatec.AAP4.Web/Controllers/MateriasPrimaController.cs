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
    public class MateriasPrimaController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: MateriasPrima
        public ActionResult Index()
        {
            return View(db.materia_prima.ToList());
        }

        // GET: MateriasPrima/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            materia_prima materia_prima = db.materia_prima.Find(id);
            if (materia_prima == null)
            {
                return HttpNotFound();
            }
            return View(materia_prima);
        }

        // GET: MateriasPrima/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MateriasPrima/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_matprima,descricao_matprima")] materia_prima materia_prima)
        {
            if (ModelState.IsValid)
            {
                db.materia_prima.Add(materia_prima);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(materia_prima);
        }

        // GET: MateriasPrima/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            materia_prima materia_prima = db.materia_prima.Find(id);
            if (materia_prima == null)
            {
                return HttpNotFound();
            }
            return View(materia_prima);
        }

        // POST: MateriasPrima/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_matprima,descricao_matprima")] materia_prima materia_prima)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materia_prima).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(materia_prima);
        }

        // GET: MateriasPrima/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            materia_prima materia_prima = db.materia_prima.Find(id);
            if (materia_prima == null)
            {
                return HttpNotFound();
            }
            return View(materia_prima);
        }

        // POST: MateriasPrima/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            materia_prima materia_prima = db.materia_prima.Find(id);
            db.materia_prima.Remove(materia_prima);
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
