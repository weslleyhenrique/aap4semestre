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
    
    public partial class materia_prima
    {
        public materia_prima()
        {
            this.estoque_materiaprima = new HashSet<estoque_materiaprima>();
            this.fornece_materiaprima = new HashSet<fornece_materiaprima>();
        }
    
        public int id_matprima { get; set; }
        public string descricao_matprima { get; set; }
    
        public virtual ICollection<estoque_materiaprima> estoque_materiaprima { get; set; }
        public virtual ICollection<fornece_materiaprima> fornece_materiaprima { get; set; }
    }
}
