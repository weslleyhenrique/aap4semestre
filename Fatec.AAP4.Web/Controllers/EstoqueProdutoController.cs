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
    public class EstoqueProdutoController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: EstoqueProduto
        public ActionResult Index()
        {
            var estoque_produtoacabado = db.estoque_produtoacabado.Include(e => e.plano_contas).Include(e => e.produto);
            return View(estoque_produtoacabado.ToList());
        }

        // GET: EstoqueProduto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estoque_produtoacabado estoque_produtoacabado = db.estoque_produtoacabado.Find(id);
            if (estoque_produtoacabado == null)
            {
                return HttpNotFound();
            }
            return View(estoque_produtoacabado);
        }

        // GET: EstoqueProduto/Create
        public ActionResult Create()
        {
            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas");
            ViewBag.id_produto = new SelectList(db.produto, "id_produto", "descricao_produto");
            return View();
        }

        // POST: EstoqueProduto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_estoque_prodacab,id_produto,id_planocontas,data_fabricacao,data_estocagem,quant_minima,quant_maxima,quant_atual")] estoque_produtoacabado estoque_produtoacabado)
        {

            var produdo = db.produto.SingleOrDefault(x => x.id_produto == estoque_produtoacabado.id_produto);

            var materiaAtual = db.estoque_materiaprima.SingleOrDefault(x => x.id_matprima == produdo.id_matprima).quant_atual;
            db.estoque_materiaprima.SingleOrDefault(x => x.id_matprima == produdo.id_matprima).quant_atual = materiaAtual - (produdo.QtdeMateriaUsada * estoque_produtoacabado.quant_atual);



            if (ModelState.IsValid)
            {
                db.estoque_produtoacabado.Add(estoque_produtoacabado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", estoque_produtoacabado.id_planocontas);
            ViewBag.id_produto = new SelectList(db.produto, "id_produto", "descricao_produto", estoque_produtoacabado.id_produto);
            return View(estoque_produtoacabado);
        }

        // GET: EstoqueProduto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estoque_produtoacabado estoque_produtoacabado = db.estoque_produtoacabado.Find(id);
            if (estoque_produtoacabado == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", estoque_produtoacabado.id_planocontas);
            ViewBag.id_produto = new SelectList(db.produto, "id_produto", "descricao_produto", estoque_produtoacabado.id_produto);
            
            return View(estoque_produtoacabado);
        }

        // POST: EstoqueProduto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_estoque_prodacab,id_produto,id_planocontas,data_fabricacao,data_estocagem,quant_minima,quant_maxima,quant_atual")] estoque_produtoacabado estoque_produtoacabado)
        {

            var produdo = db.produto.SingleOrDefault(x => x.id_produto == estoque_produtoacabado.id_produto);
            var materiaAtual = db.estoque_materiaprima.SingleOrDefault(x => x.id_matprima == produdo.id_matprima).quant_atual;


            var qtdeMateria = produdo.QtdeMateriaUsada * estoque_produtoacabado.quant_atual;

            if (qtdeMateria > materiaAtual)
            {
                ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", estoque_produtoacabado.id_planocontas);
                ViewBag.id_produto = new SelectList(db.produto, "id_produto", "descricao_produto", estoque_produtoacabado.id_produto);
                ViewBag.Atual = materiaAtual;
                ViewBag.Requirida = qtdeMateria;
                ViewBag.necessario = qtdeMateria - materiaAtual;
                ViewBag.SemMateriaPrima = 1;
                return View(estoque_produtoacabado);
            }


            db.estoque_materiaprima.SingleOrDefault(x => x.id_matprima == produdo.id_matprima).quant_atual = materiaAtual - qtdeMateria;



            if (ModelState.IsValid)
            {
                db.Entry(estoque_produtoacabado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", estoque_produtoacabado.id_planocontas);
            ViewBag.id_produto = new SelectList(db.produto, "id_produto", "descricao_produto", estoque_produtoacabado.id_produto);
            return View(estoque_produtoacabado);
        }

        // GET: EstoqueProduto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estoque_produtoacabado estoque_produtoacabado = db.estoque_produtoacabado.Find(id);
            if (estoque_produtoacabado == null)
            {
                return HttpNotFound();
            }
            return View(estoque_produtoacabado);
        }

        // POST: EstoqueProduto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            estoque_produtoacabado estoque_produtoacabado = db.estoque_produtoacabado.Find(id);

            var produdo = db.produto.SingleOrDefault(x => x.id_produto == estoque_produtoacabado.id_produto);

            var materiaAtual = db.estoque_materiaprima.SingleOrDefault(x => x.id_matprima == produdo.id_matprima).quant_atual;
            db.estoque_materiaprima.SingleOrDefault(x => x.id_matprima == produdo.id_matprima).quant_atual = materiaAtual + (produdo.QtdeMateriaUsada * estoque_produtoacabado.quant_atual);



            db.estoque_produtoacabado.Remove(estoque_produtoacabado);
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
