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
    
    public partial class contas_pagar
    {
        public int id_contaspagar { get; set; }
        public int id_fornecedor { get; set; }
        public int id_planocontas { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
        public string descricao_conta { get; set; }
        public string valor_conta { get; set; }
        public Nullable<System.DateTime> data_vencimento { get; set; }
        public string status_conta { get; set; }
        public string observacao { get; set; }
    
        public virtual plano_contas plano_contas { get; set; }
        public virtual fornecedor fornecedor { get; set; }
    }
}
