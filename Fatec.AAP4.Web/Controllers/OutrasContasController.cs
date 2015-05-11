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
    public class OutrasContasController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: OutrasContas
        public ActionResult Index()
        {
            var outras_contas = db.outras_contas.Include(o => o.plano_contas);
            return View(outras_contas.ToList());
        }

        // GET: OutrasContas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            outras_contas outras_contas = db.outras_contas.Find(id);
            if (outras_contas == null)
            {
                return HttpNotFound();
            }
            return View(outras_contas);
        }

        // GET: OutrasContas/Create
        public ActionResult Create()
        {
            ViewBag.Plano_Contas_id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas");
            return View();
        }

        // POST: OutrasContas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_conta,nome_conta,descricao_conta,data_cadastro,Plano_Contas_id_planocontas")] outras_contas outras_contas)
        {
            if (ModelState.IsValid)
            {
                db.outras_contas.Add(outras_contas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Plano_Contas_id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", outras_contas.Plano_Contas_id_planocontas);
            return View(outras_contas);
        }

        // GET: OutrasContas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            outras_contas outras_contas = db.outras_contas.Find(id);
            if (outras_contas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Plano_Contas_id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", outras_contas.Plano_Contas_id_planocontas);
            return View(outras_contas);
        }

        // POST: OutrasContas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_conta,nome_conta,descricao_conta,data_cadastro,Plano_Contas_id_planocontas")] outras_contas outras_contas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outras_contas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Plano_Contas_id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", outras_contas.Plano_Contas_id_planocontas);
            return View(outras_contas);
        }

        // GET: OutrasContas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            outras_contas outras_contas = db.outras_contas.Find(id);
            if (outras_contas == null)
            {
                return HttpNotFound();
            }
            return View(outras_contas);
        }

        // POST: OutrasContas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            outras_contas outras_contas = db.outras_contas.Find(id);
            db.outras_contas.Remove(outras_contas);
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
