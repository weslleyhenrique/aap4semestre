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
    public class PedidosController : Controller
    {
        private mydbEntities db = new mydbEntities();
        public DetalheItens DetalheSelecionado = new DetalheItens();

        // GET: Pedidos
        public ActionResult Index()
        {
            var pedido = db.pedido.Include(p => p.cliente);
            PedidosViewModel pedidos = new PedidosViewModel();
            pedidos.ItensPedidos = db.item_pedido.ToList();
            pedidos.Pedidos = db.pedido.ToList();

            return View(pedidos);
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pedido pedido = db.pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }

            DetalheSelecionado.EstoqueProd = db.estoque_produtoacabado.Where(x => x.quant_atual > 0).ToList();
            DetalheSelecionado.ListEstoqueProd = DetalheSelecionado.EstoqueProd.Select(x => new
                SelectListItem { Text = x.produto.descricao_produto+" - ( "+x.quant_atual+" )", Value = x.id_produto.ToString() });
            DetalheSelecionado.ItensDB = db.item_pedido.Where(x => x.id_item_pedido == pedido.idPedido).ToList();
            DetalheSelecionado.Produtos = db.produto.ToList();
            DetalheSelecionado.PedidoSelecionado = pedido;
            DetalheSelecionado.Quantidade = 1;

            ViewBag.ProdId = new SelectList
                     (
                         new DetalheItens().FuncEstoqueProd(),
                         "id_estoque_prodacab",
                         "produto.descricao_produto"
                     );
            return View(DetalheSelecionado);
        }



        //public ActionResult IncluirProd()
        //{
        //    // Criando uma ViewBag com uma lista de clientes.
        //    // Utilizo o nome da ViewBag com ClienteId apenas
        //    // para facilitar o entendimento do código
        //    // new SelectList significa retornar uma
        //    // estrutura de DropDownList
        //    ViewBag.ProdId = new SelectList
        //        (
        //            new DetalheItens().FuncEstoqueProd() ,
        //            "id_estoque_prodacab",
        //            "produto.descricao_produto"
        //        );

        //    return View();
        //}

        //[HttpPost]
        // No Post o valor selecionado do DropDownList
        // será recebido no parametro clienteId
        public ActionResult IncluirProd(DetalheItens model)
        {
            // O quarto parametro "clienteId" diz qual é o ID
            // que deve vir pré-selecionado ao montar o DropDownList
            int qdte = 0;
            var valorProduto = Convert.ToDecimal(db.produto.SingleOrDefault(x => x.id_produto == model.ItemSelecionado).valor_unitario);
            if (ModelState.IsValid)
            {

                var qtdeAtual = db.estoque_produtoacabado.SingleOrDefault(x => x.id_produto == model.ItemSelecionado).quant_atual;

                if (model.Quantidade > qtdeAtual)
                {
                    qdte = Convert.ToInt32(qtdeAtual);
                }
                else
                {
                    qdte = model.Quantidade;
                    
                }

                db.estoque_produtoacabado.SingleOrDefault(x => x.id_produto == model.ItemSelecionado).quant_atual = qtdeAtual - qdte;
               
                var produdo = db.produto.SingleOrDefault(x => x.id_produto == model.ItemSelecionado);

                //var materiaAtual = db.estoque_materiaprima.SingleOrDefault(x => x.id_matprima == produdo.id_matprima).quant_atual;
                //db.estoque_materiaprima.SingleOrDefault(x => x.id_matprima == produdo.id_matprima).quant_atual = materiaAtual - (produdo.QtdeMateriaUsada * model.Quantidade);

                db.item_pedido.Add(
                    new item_pedido
                    {
                        id_item_pedido = model.PedidoSelecionado.idPedido,
                        id_produto_fk = model.ItemSelecionado,
                        quantidade = qdte,
                        valor_unitario_item = valorProduto,
                        valor_total_item = qdte * valorProduto
                    });
                db.SaveChanges();
                return RedirectToAction("Details", new {id = model.PedidoSelecionado.idPedido });
            }



            return View();
        }



        public ActionResult ConfirmarVenda(string idPedido, string FormaPagamento)
        {


            contas_receber pagamento = new contas_receber();
            pagamento.data_cadastro = DateTime.Now;
            pagamento.data_recebimento = DateTime.Now;
            pagamento.status = "OK";
            pagamento.forma_pagamento = FormaPagamento;
            var plano = db.plano_contas.ToList();
            pagamento.id_planocontas = plano[0].id_planocontas;
            pagamento.idPedido = Convert.ToInt32(idPedido);
            int id = Convert.ToInt32(idPedido);
            pagamento.idContas_Receber = Convert.ToInt32(idPedido);
            var total = db.item_pedido.Where(x => x.id_item_pedido == id).Sum(x => x.valor_total_item);
            pagamento.Valor_receber = total;

            db.pedido.SingleOrDefault(x => x.idPedido == id).IsConfirmado = 1;
            db.pedido.SingleOrDefault(x => x.idPedido == id).data_compra = DateTime.Now;


            db.contas_receber.Add(pagamento);
            db.SaveChanges();
            return RedirectToAction("Index");

        }








        // GET: Pedidos/Create
        public ActionResult Create()
        {
            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaosocial");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPedido,idCliente,data_compra,valor_total,id_item_pedido_fk")] pedido pedido)
        {
            Random random = new Random();
            pedido.idPedido = random.Next(100000, 999999);
            pedido.id_item_pedido_fk = random.Next(100000, 999999);
            if (ModelState.IsValid)
            {
                db.pedido.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaosocial", pedido.idCliente);
            return View(pedido);
        }





        public ActionResult DeletarItem(int pedidoId, int key,int produtoId)
        {

            item_pedido item_pedido = db.item_pedido.SingleOrDefault(
                x => x.id_item_pedido == pedidoId && x.key_pedido == key);
            if (item_pedido != null)
            {

                var produdo = db.produto.SingleOrDefault(x => x.id_produto == produtoId);

                var materiaAtual = db.estoque_materiaprima.SingleOrDefault(x => x.id_matprima == produdo.id_matprima).quant_atual;
                db.estoque_materiaprima.SingleOrDefault(x => x.id_matprima == produdo.id_matprima).quant_atual = materiaAtual + (produdo.QtdeMateriaUsada * item_pedido.quantidade);


                var qtdeAtual = db.estoque_produtoacabado.SingleOrDefault(x => x.id_produto == produtoId).quant_atual;
                db.estoque_produtoacabado.SingleOrDefault(x => x.id_produto == produtoId).quant_atual = qtdeAtual + item_pedido.quantidade;

                db.item_pedido.Remove(item_pedido);
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = pedidoId });
        }















        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pedido pedido = db.pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaosocial", pedido.idCliente);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPedido,idCliente,data_compra,valor_total,id_item_pedido_fk")] pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaosocial", pedido.idCliente);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pedido pedido = db.pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            pedido pedido = db.pedido.Find(id);

            if (pedido.IsConfirmado != null && pedido.IsConfirmado != 0)
            {
                contas_receber receber = db.contas_receber.SingleOrDefault(x => x.idContas_Receber == id);
                db.contas_receber.Remove(receber);
            }


            foreach (var item in db.item_pedido)
            {
                if (item.id_item_pedido == id)
                {
                    db.item_pedido.Remove(item);
                }
            }

            db.pedido.Remove(pedido);
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
