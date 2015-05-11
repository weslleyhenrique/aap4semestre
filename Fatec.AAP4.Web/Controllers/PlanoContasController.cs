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
    public class PlanoContasController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: PlanoContas
        public ActionResult Index()
        {
            var plano_contas = db.plano_contas.Include(p => p.dre);
            return View(plano_contas.ToList());
        }

        // GET: PlanoContas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plano_contas plano_contas = db.plano_contas.Find(id);
            if (plano_contas == null)
            {
                return HttpNotFound();
            }
            return View(plano_contas);
        }

        // GET: PlanoContas/Create
        public ActionResult Create()
        {
            ViewBag.id_dre = new SelectList(db.dre, "id_dre", "nome_dre");
            return View();
        }

        // POST: PlanoContas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_planocontas,id_dre,codigo_planocontas,descricao_planocontas")] plano_contas plano_contas)
        {
            if (ModelState.IsValid)
            {
                db.plano_contas.Add(plano_contas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_dre = new SelectList(db.dre, "id_dre", "nome_dre", plano_contas.id_dre);
            return View(plano_contas);
        }

        // GET: PlanoContas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plano_contas plano_contas = db.plano_contas.Find(id);
            if (plano_contas == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_dre = new SelectList(db.dre, "id_dre", "nome_dre", plano_contas.id_dre);
            return View(plano_contas);
        }

        // POST: PlanoContas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_planocontas,id_dre,codigo_planocontas,descricao_planocontas")] plano_contas plano_contas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plano_contas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_dre = new SelectList(db.dre, "id_dre", "nome_dre", plano_contas.id_dre);
            return View(plano_contas);
        }

        // GET: PlanoContas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plano_contas plano_contas = db.plano_contas.Find(id);
            if (plano_contas == null)
            {
                return HttpNotFound();
            }
            return View(plano_contas);
        }

        // POST: PlanoContas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            plano_contas plano_contas = db.plano_contas.Find(id);
            db.plano_contas.Remove(plano_contas);
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
