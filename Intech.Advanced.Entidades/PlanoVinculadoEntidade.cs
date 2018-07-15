using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
    [Table("FI_PLANO_VINCULADO")]
    public class PlanoVinculadoEntidade
    {
		public int SQ_CONTRATO_TRABALHO { get; set; }
		public int SQ_PLANO_PREVIDENCIAL { get; set; }
		public string NR_INSCRICAO { get; set; }
		public int? SQ_SIT_PLANO { get; set; }
		public DateTime? DT_SITUACAO { get; set; }
		public int? SQ_MOT_SIT_PLANO { get; set; }
		public string NR_IDENT_BANCARIO { get; set; }
		public DateTime? DT_DEFERIMENTO { get; set; }
		public DateTime DT_INSC_PLANO { get; set; }
		public int? SQ_SIT_INSCRICAO { get; set; }
		public DateTime? DT_PRIMEIRA_CONTRIB { get; set; }
		public DateTime? DT_VENC_CARENCIA { get; set; }
		public decimal? PC_TAXA_MAXIMA { get; set; }
		public int? SQ_OPCAO_TRIBUTACAO { get; set; }
		public DateTime? DT_OPCAO_TRIBUTACAO { get; set; }
		public string IR_FUNDADOR { get; set; }
		public string IR_GRUPO { get; set; }
		public DateTime? DT_INSC_ANTERIOR { get; set; }
		public DateTime? DT_ESPERA { get; set; }
		public decimal? VL_RES_MATEMATICA { get; set; }
		public int? QT_PRAZO_JOIA { get; set; }
		public DateTime? DT_OBITO { get; set; }
		public string DS_PLANO_PREVIDENCIAL { get; set; }
		public string DS_SIT_PLANO { get; set; }
		public string DS_SIT_INSCRICAO { get; set; }
		public string DS_MOT_SIT_PLANO { get; set; }
		public string NR_CODIGO_CNPB { get; set; }
		public string CD_INDICE_VALORIZACAO { get; set; }
        
    }
}
