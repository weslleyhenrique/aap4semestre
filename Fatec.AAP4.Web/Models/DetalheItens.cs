using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fatec.AAP4.Web.Models
{
    public class DetalheItens
    {
        protected mydbEntities db = new mydbEntities();

        public IEnumerable<item_pedido> ItensDB { get; set; }
        public IEnumerable<estoque_produtoacabado> EstoqueProd { get; set; }

        public pedido PedidoSelecionado { get; set; }


        public IEnumerable<estoque_produtoacabado> FuncEstoqueProd()
        {
            return db.estoque_produtoacabado.ToList();
        }


    }
}