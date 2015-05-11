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
    public class ForneceMateriasController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: ForneceMaterias
        public ActionResult Index()
        {
            var fornece_materiaprima = db.fornece_materiaprima.Include(f => f.fornecedor).Include(f => f.materia_prima);
            return View(fornece_materiaprima.ToList());
        }

        // GET: ForneceMaterias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fornece_materiaprima fornece_materiaprima = db.fornece_materiaprima.Find(id);
            if (fornece_materiaprima == null)
            {
                return HttpNotFound();
            }
            return View(fornece_materiaprima);
        }

        // GET: ForneceMaterias/Create
        public ActionResult Create()
        {
            ViewBag.id_fornecedor = new SelectList(db.fornecedor, "id_fornecedor", "cnpj");
            ViewBag.id_matprima = new SelectList(db.materia_prima, "id_matprima", "descricao_matprima");
            return View();
        }

        // POST: ForneceMaterias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_fornecedor,id_matprima,id_fornecedor_materiaprima")] fornece_materiaprima fornece_materiaprima)
        {
            if (ModelState.IsValid)
            {
                db.fornece_materiaprima.Add(fornece_materiaprima);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_fornecedor = new SelectList(db.fornecedor, "id_fornecedor", "cnpj", fornece_materiaprima.id_fornecedor);
            ViewBag.id_matprima = new SelectList(db.materia_prima, "id_matprima", "descricao_matprima", fornece_materiaprima.id_matprima);
            return View(fornece_materiaprima);
        }

        // GET: ForneceMaterias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fornece_materiaprima fornece_materiaprima = db.fornece_materiaprima.Find(id);
            if (fornece_materiaprima == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_fornecedor = new SelectList(db.fornecedor, "id_fornecedor", "cnpj", fornece_materiaprima.id_fornecedor);
            ViewBag.id_matprima = new SelectList(db.materia_prima, "id_matprima", "descricao_matprima", fornece_materiaprima.id_matprima);
            return View(fornece_materiaprima);
        }

        // POST: ForneceMaterias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_fornecedor,id_matprima,id_fornecedor_materiaprima")] fornece_materiaprima fornece_materiaprima)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fornece_materiaprima).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_fornecedor = new SelectList(db.fornecedor, "id_fornecedor", "cnpj", fornece_materiaprima.id_fornecedor);
            ViewBag.id_matprima = new SelectList(db.materia_prima, "id_matprima", "descricao_matprima", fornece_materiaprima.id_matprima);
            return View(fornece_materiaprima);
        }

        // GET: ForneceMaterias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fornece_materiaprima fornece_materiaprima = db.fornece_materiaprima.Find(id);
            if (fornece_materiaprima == null)
            {
                return HttpNotFound();
            }
            return View(fornece_materiaprima);
        }

        // POST: ForneceMaterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fornece_materiaprima fornece_materiaprima = db.fornece_materiaprima.Find(id);
            db.fornece_materiaprima.Remove(fornece_materiaprima);
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
