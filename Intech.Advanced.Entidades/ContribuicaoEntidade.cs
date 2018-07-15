using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
    [Table("FI_FICHA_CONTRIB_PREVIDENCIAL")]
    public class ContribuicaoEntidade
    {
		public int SQ_PLANO_PREVIDENCIAL { get; set; }
		public int SQ_CONTRATO_TRABALHO { get; set; }
		public int SQ_TIPO_FUNDO { get; set; }
		public int SQ_TIPO_COBRANCA { get; set; }
		public DateTime DT_REFERENCIA { get; set; }
		public DateTime DT_COMPETENCIA { get; set; }
		public decimal? VL_CONTRIBUICAO { get; set; }
		public decimal? QT_COTA_CONTRIBUICAO { get; set; }
		public decimal? VL_BASE_FUNDACAO { get; set; }
		public decimal? VL_BASE_PREVIDENCIA { get; set; }
		public DateTime? DT_APORTE { get; set; }
		public decimal? VL_COTA { get; set; }
		public string IR_OPERACAO { get; set; }
		public int IR_LANCAMENTO { get; set; }
		public int? ID_CONTRIBUICAO { get; set; }
		public int? SQ_ORIGEM { get; set; }
		public string DS_TIPO_COBRANCA { get; set; }
		public string DS_TIPO_FUNDO { get; set; }
        
    }
}
