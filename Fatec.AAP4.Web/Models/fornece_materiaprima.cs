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
    
    public partial class fornece_materiaprima
    {
        public int id_fornecedor { get; set; }
        public int id_matprima { get; set; }
        public int id_fornecedor_materiaprima { get; set; }
    
        public virtual fornecedor fornecedor { get; set; }
        public virtual materia_prima materia_prima { get; set; }
    }
}
