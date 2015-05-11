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
    public class EstoqueMateriaController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: EstoqueMateria
        public ActionResult Index()
        {
            var estoque_materiaprima = db.estoque_materiaprima.Include(e => e.plano_contas).Include(e => e.materia_prima);
            return View(estoque_materiaprima.ToList());
        }

        // GET: EstoqueMateria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estoque_materiaprima estoque_materiaprima = db.estoque_materiaprima.Find(id);
            if (estoque_materiaprima == null)
            {
                return HttpNotFound();
            }
            return View(estoque_materiaprima);
        }

        // GET: EstoqueMateria/Create
        public ActionResult Create()
        {
            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas");
            ViewBag.id_matprima = new SelectList(db.materia_prima, "id_matprima", "descricao_matprima");
            return View();
        }

        // POST: EstoqueMateria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_estoque_matprima,id_matprima,id_planocontas,data_estocagem,quant_minima,quant_maxima,quant_atual")] estoque_materiaprima estoque_materiaprima)
        {
            if (ModelState.IsValid)
            {
                db.estoque_materiaprima.Add(estoque_materiaprima);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", estoque_materiaprima.id_planocontas);
            ViewBag.id_matprima = new SelectList(db.materia_prima, "id_matprima", "descricao_matprima", estoque_materiaprima.id_matprima);
            return View(estoque_materiaprima);
        }

        // GET: EstoqueMateria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estoque_materiaprima estoque_materiaprima = db.estoque_materiaprima.Find(id);
            if (estoque_materiaprima == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", estoque_materiaprima.id_planocontas);
            ViewBag.id_matprima = new SelectList(db.materia_prima, "id_matprima", "descricao_matprima", estoque_materiaprima.id_matprima);
            return View(estoque_materiaprima);
        }

        // POST: EstoqueMateria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_estoque_matprima,id_matprima,id_planocontas,data_estocagem,quant_minima,quant_maxima,quant_atual")] estoque_materiaprima estoque_materiaprima)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estoque_materiaprima).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_planocontas = new SelectList(db.plano_contas, "id_planocontas", "codigo_planocontas", estoque_materiaprima.id_planocontas);
            ViewBag.id_matprima = new SelectList(db.materia_prima, "id_matprima", "descricao_matprima", estoque_materiaprima.id_matprima);
            return View(estoque_materiaprima);
        }

        // GET: EstoqueMateria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estoque_materiaprima estoque_materiaprima = db.estoque_materiaprima.Find(id);
            if (estoque_materiaprima == null)
            {
                return HttpNotFound();
            }
            return View(estoque_materiaprima);
        }

        // POST: EstoqueMateria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            estoque_materiaprima estoque_materiaprima = db.estoque_materiaprima.Find(id);
            db.estoque_materiaprima.Remove(estoque_materiaprima);
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
