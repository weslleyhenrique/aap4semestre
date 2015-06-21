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
    public class ItensPedidoController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: ItensPedido
        public ActionResult Index()
        {
            var item_pedido = db.item_pedido;
            return View(item_pedido.ToList());
        }

        // GET: ItensPedido/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            item_pedido item_pedido = db.item_pedido.Find(id);
            if (item_pedido == null)
            {
                return HttpNotFound();
            }
            return View(item_pedido);
        }

        // GET: ItensPedido/Create
        public ActionResult Create()
        {
            ViewBag.id_produto_fk = new SelectList(db.produto, "id_produto", "descricao_produto");
            return View();
        }

        // POST: ItensPedido/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_item_pedido,valor_unitario_item,quantidade,valor_total_item,id_produto_fk,key_item")] item_pedido item_pedido)
        {
            if (ModelState.IsValid)
            {
                db.item_pedido.Add(item_pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_produto_fk = new SelectList(db.produto, "id_produto", "descricao_produto", item_pedido.id_produto_fk);
            return View(item_pedido);
        }

        // GET: ItensPedido/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            item_pedido item_pedido = db.item_pedido.Find(id);
            if (item_pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_produto_fk = new SelectList(db.produto, "id_produto", "descricao_produto", item_pedido.id_produto_fk);
            return View(item_pedido);
        }

        // POST: ItensPedido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_item_pedido,valor_unitario_item,quantidade,valor_total_item,id_produto_fk,key_item")] item_pedido item_pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item_pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_produto_fk = new SelectList(db.produto, "id_produto", "descricao_produto", item_pedido.id_produto_fk);
            return View(item_pedido);
        }

        // GET: ItensPedido/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            item_pedido item_pedido = db.item_pedido.Find(id);
            if (item_pedido == null)
            {
                return HttpNotFound();
            }
            return View(item_pedido);
        }

        // POST: ItensPedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            item_pedido item_pedido = db.item_pedido.Find(id);
            db.item_pedido.Remove(item_pedido);
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
