using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fatec.AAP4.Web.Models
{
    public class DetalheItens
    {
        public mydbEntities db = new mydbEntities();

        public IEnumerable<item_pedido> ItensDB { get; set; }
        public IEnumerable<estoque_produtoacabado> EstoqueProd { get; set; }
        public IEnumerable<produto> Produtos { get; set; }
        public IEnumerable<SelectListItem> ListEstoqueProd { get; set; }

        public int ItemSelecionado { get; set; }
        public int Quantidade { get; set; }

        public pedido PedidoSelecionado { get; set; }


        public IEnumerable<estoque_produtoacabado> FuncEstoqueProd()
        {
            return db.estoque_produtoacabado.ToList();
        }


    }
}