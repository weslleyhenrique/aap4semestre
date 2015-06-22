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
    public class ContasReceberController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: ContasReceber
        public ActionResult Index()
        {
            var contas_receber = db.contas_receber.Include(c => c.plano_contas).Include(c => c.pedido);
            return View(contas_receber.ToList());
        }

        // GET: ContasReceber/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contas_receber contas_receber = db.contas_receber.Find(id);
            if (contas_receber == null)
            {
                return HttpNotFound();
            }
            return View(contas_receber);
        }

        // GET: ContasReceber/Create
        public ActionResult Create()
        {
            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas");
            ViewBag.idPedido = new SelectList(db.pedido, "idPedido", "idPedido");
            return View();
        }

        // POST: ContasReceber/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idContas_Receber,idPedido,id_planocontas,data_cadastro,data_recebimento,status,forma_pagamento,Valor_receber")] contas_receber contas_receber)
        {
            if (ModelState.IsValid)
            {
                db.contas_receber.Add(contas_receber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", contas_receber.id_planocontas);
            ViewBag.idPedido = new SelectList(db.pedido, "idPedido", "idPedido", contas_receber.idPedido);
            return View(contas_receber);
        }

        // GET: ContasReceber/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contas_receber contas_receber = db.contas_receber.Find(id);
            if (contas_receber == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", contas_receber.id_planocontas);
            ViewBag.idPedido = new SelectList(db.pedido, "idPedido", "idPedido", contas_receber.idPedido);
            return View(contas_receber);
        }

        // POST: ContasReceber/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idContas_Receber,idPedido,id_planocontas,data_cadastro,data_recebimento,status,forma_pagamento,Valor_receber")] contas_receber contas_receber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contas_receber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", contas_receber.id_planocontas);
            ViewBag.idPedido = new SelectList(db.pedido, "idPedido", "idPedido", contas_receber.idPedido);
            return View(contas_receber);
        }

        // GET: ContasReceber/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contas_receber contas_receber = db.contas_receber.Find(id);
            if (contas_receber == null)
            {
                return HttpNotFound();
            }
            return View(contas_receber);
        }

        // POST: ContasReceber/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contas_receber contas_receber = db.contas_receber.Find(id);
            db.contas_receber.Remove(contas_receber);
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
