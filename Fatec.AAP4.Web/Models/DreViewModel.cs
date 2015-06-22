using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fatec.AAP4.Web.Models
{
    public class DreViewModel
    {


        public decimal TotalVendasBrutas { get; set; }
        public decimal  TotalCMV { get; set; }
        public IEnumerable<contas_pagar> DespesasOperacionais { get; set; }
        public decimal TotalDespesasOperacionais { get; set; }
        public IEnumerable<outras_contas> DespesasFinanceiras { get; set; }
        public decimal TotalDespesasFinanceiras { get; set; }

        public decimal ReceitaLiquida { get; set; }
        public decimal LucroBruto { get; set; }
        public decimal LucroLiquido { get; set; }
        public decimal IR { get; set; }
        public decimal LAJIR { get; set; }
        public decimal LAIR { get; set; }

        public decimal TotalEstoqueMateria { get; set; }
        public decimal TotalImpostos { get; set; }
        public dre DreSelecionado { get; set; }





    }
}