using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
    [Table("FI_HIST_MANUT_CONTRIBUICAO")]
    public class HistManutContribuicaoEntidade
    {
		[Key]
		public int SQ_MANUTENCAO { get; set; }
		public int SQ_PLANO_PREVIDENCIAL { get; set; }
		public int SQ_CONTRATO_TRABALHO { get; set; }
		public DateTime DT_INIC_VALIDADE { get; set; }
		public DateTime? DT_TERM_VALIDADE { get; set; }
		public int? SQ_TIPO_FUNDO { get; set; }
		public int SQ_OPCAO_COBRANCA { get; set; }
		public int? SQ_REGRA { get; set; }
		public decimal? VL_COEF_TAXA { get; set; }
		public int? SQ_REGRA_LIMITE { get; set; }
		public int? ID_CONTRIBUICAO { get; set; }
        
    }
}
