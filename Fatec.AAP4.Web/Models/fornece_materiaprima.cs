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
    using System.ComponentModel.DataAnnotations;
    
    public partial class fornece_materiaprima
    {
        [Display(Name = "C�digo Fornecedor")]
        public int id_fornecedor { get; set; }
        [Display(Name = "C�digo Materia-Prima")]
        public int id_matprima { get; set; }
        [Display(Name = "C�digo Fornecedor por Materia-Prima")]
        public int id_fornecedor_materiaprima { get; set; }
    
        public virtual fornecedor fornecedor { get; set; }
        public virtual materia_prima materia_prima { get; set; }
    }
}
