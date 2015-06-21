using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fatec.AAP4.Web.Models
{
    public class PedidosViewModel
    {

        public IEnumerable<pedido> Pedidos { get; set; }
        public IEnumerable<item_pedido> ItensPedidos { get; set; }


    }
}