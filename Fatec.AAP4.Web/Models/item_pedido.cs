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
    
    public partial class item_pedido
    {
        public int id_item_pedido { get; set; }
        public Nullable<double> valor_unitario_item { get; set; }
        public Nullable<int> quantidade { get; set; }
        public Nullable<double> valor_total_item { get; set; }
        public int id_produto_fk { get; set; }
        public int key_item { get; set; }
    
        public virtual produto produto { get; set; }
    }
}
