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

            DreViewModel Dre = new DreViewModel();
            var prod = db.produto.ToList();

            decimal bruto = 0.00m;

            var itens = db.item_pedido.ToList();
            foreach (var item in itens)
            {
                bruto = bruto + item.valor_total_item;
            }
            Dre.TotalVendasBrutas = Convert.ToDecimal(bruto);

            decimal totalimposto = 0;
            foreach (var item in db.item_pedido)
            {
                var imposto = prod.SingleOrDefault(x => x.id_produto == item.id_produto_fk).ValorImpostos;
                totalimposto = totalimposto + imposto * Convert.ToDecimal(item.quantidade);
            }
            Dre.TotalImpostos = totalimposto;
            Dre.ReceitaLiquida = Dre.TotalVendasBrutas - Dre.TotalImpostos;


            decimal totalcusto = 0;
            foreach (var item in db.item_pedido)
            {
                var custo = prod.SingleOrDefault(x => x.id_produto == item.id_produto_fk).PrecoCusto;
                totalcusto = totalcusto + custo * Convert.ToDecimal(item.quantidade);
            }
            Dre.TotalCMV = totalcusto;
            Dre.LucroBruto = Dre.ReceitaLiquida - Dre.TotalCMV;


            decimal operacional = 0.00m;

            var oper = db.contas_pagar.ToList();
            foreach (var item in oper)
            {
                operacional = operacional + item.valor_conta;
            }
            Dre.TotalDespesasOperacionais = operacional;
            Dre.DespesasOperacionais = db.contas_pagar.ToList();

            Dre.LAJIR = Dre.LucroBruto - Dre.TotalDespesasOperacionais;

            decimal financ = 0.00m;
            var Listfinanc = db.outras_contas.ToList();
            foreach (var item in Listfinanc)
            {
                financ = financ + item.valor;
            }
            Dre.TotalDespesasFinanceiras = financ;


            Dre.DespesasFinanceiras = db.outras_contas.ToList();

            Dre.LAIR = Dre.LAJIR - Dre.TotalDespesasFinanceiras;

            Dre.IR = Dre.LAJIR * 0.30m;

            Dre.LucroLiquido = Dre.LAIR - Dre.IR;


            Dre.DreSelecionado = db.dre.Find(id);
            if (Dre.DreSelecionado == null)
            {
                return HttpNotFound();
            }
            return View(Dre);
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
