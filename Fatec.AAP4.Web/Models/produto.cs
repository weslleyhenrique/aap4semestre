//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fatec.AAP4.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class produto
    {
        public produto()
        {
            this.estoque_produtoacabado = new HashSet<estoque_produtoacabado>();
            this.item_pedido = new HashSet<item_pedido>();
        }
    
        public int id_produto { get; set; }
        public string descricao_produto { get; set; }
        public string valor_unitario { get; set; }
    
        public virtual ICollection<estoque_produtoacabado> estoque_produtoacabado { get; set; }
        public virtual ICollection<item_pedido> item_pedido { get; set; }
    }
}