using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
	[Table("FI_HIST_VALORES_PROCESSO")]
	public class HistValoresProcessoEntidade
	{
		public int SQ_PROCESSO { get; set; }
		public DateTime DT_INIC_VALIDADE { get; set; }
		public int NR_VERSAO { get; set; }
		public int? SQ_OPCAO_RECEBIMENTO { get; set; }
		public int? SQ_MOT_ALTERACAO { get; set; }
		public string EE_BONUS_SUPLEMENTAR { get; set; }
		public decimal? VL_RM_FUNDACAO { get; set; }
		public decimal? VL_RM_PREVIDENCIA { get; set; }
		public decimal? VL_SB_FUNDACAO { get; set; }
		public decimal? VL_SB_PREVIDENCIA { get; set; }
		public decimal? VL_PARCELA { get; set; }
		public decimal? VL_AB_FUNDACAO { get; set; }
		public decimal? VL_AB_PREVIDENCIA { get; set; }
		public decimal? VL_BENEFICIO_MINIMO { get; set; }
		public decimal? VL_MD_PREVIDENCIA { get; set; }
		public int? QT_PARCELA { get; set; }
		public DateTime? DT_REFERENCIA { get; set; }
		public int? SQ_CRONOGRAMA { get; set; }
		public decimal? PC_RESG_MENSAL { get; set; }
		public decimal? VL_SALDO_ANTERIOR { get; set; }
	}
}
