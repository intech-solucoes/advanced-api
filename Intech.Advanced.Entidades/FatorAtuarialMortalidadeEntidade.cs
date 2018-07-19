using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
    [Table("FI_FATOR_ATUARIAL_MORTALIDADE")]
    public class FatorAtuarialMortalidadeEntidade
    {
		[Key]
		public int SQ_FATOR { get; set; }
		public DateTime DT_INIC_VALIDADE { get; set; }
		public string IC_TABELA { get; set; }
		public int? NR_IDADE_TIT { get; set; }
		public int? NR_IDADE_DEP { get; set; }
		public decimal? VL_FATOR_A { get; set; }
		public decimal? VL_FATOR_B { get; set; }
        
    }
}
