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
    public class ContasPagarController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: ContasPagar
        public ActionResult Index()
        {
            var contas_pagar = db.contas_pagar.Include(c => c.plano_contas).Include(c => c.fornecedor);
            return View(contas_pagar.ToList());
        }

        // GET: ContasPagar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var conta = db.contas_pagar.SingleOrDefault(x => x.id_contaspagar == id);
            contas_pagar contas_pagar = conta;
            if (contas_pagar == null)
            {
                return HttpNotFound();
            }
            return View(contas_pagar);
        }

        // GET: ContasPagar/Create
        public ActionResult Create()
        {
            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas");
            ViewBag.id_fornecedor = new SelectList(db.fornecedor, "id_fornecedor", "cnpj");
            return View();
        }

        // POST: ContasPagar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_contaspagar,id_fornecedor,id_planocontas,data_cadastro,descricao_conta,valor_conta,data_vencimento,status_conta,observacao")] contas_pagar contas_pagar)
        {
            if (ModelState.IsValid)
            {
                db.contas_pagar.Add(contas_pagar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", contas_pagar.id_planocontas);
            ViewBag.id_fornecedor = new SelectList(db.fornecedor, "id_fornecedor", "cnpj", contas_pagar.id_fornecedor);
            return View(contas_pagar);
        }

        // GET: ContasPagar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var conta = db.contas_pagar.SingleOrDefault(x=>x.id_contaspagar==id);
            contas_pagar contas_pagar = conta;
            if (contas_pagar == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", contas_pagar.id_planocontas);
            ViewBag.id_fornecedor = new SelectList(db.fornecedor, "id_fornecedor", "cnpj", contas_pagar.id_fornecedor);
            return View(contas_pagar);
        }

        // POST: ContasPagar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_contaspagar,id_fornecedor,id_planocontas,data_cadastro,descricao_conta,valor_conta,data_vencimento,status_conta,observacao")] contas_pagar contas_pagar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contas_pagar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", contas_pagar.id_planocontas);
            ViewBag.id_fornecedor = new SelectList(db.fornecedor, "id_fornecedor", "cnpj", contas_pagar.id_fornecedor);
            return View(contas_pagar);
        }

        // GET: ContasPagar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contas_pagar contas_pagar = db.contas_pagar.SingleOrDefault(x=>x.id_contaspagar==id);
            if (contas_pagar == null)
            {
                return HttpNotFound();
            }
            return View(contas_pagar);
        }

        // POST: ContasPagar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contas_pagar contas_pagar = db.contas_pagar.SingleOrDefault(x => x.id_contaspagar == id);
            db.contas_pagar.Remove(contas_pagar);
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
