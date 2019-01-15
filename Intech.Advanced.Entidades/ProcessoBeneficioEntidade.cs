using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
    [Table("FI_PROCESSO_BENEFICIO")]
    public class ProcessoBeneficioEntidade
    {
		[Key]
		public int SQ_PROCESSO { get; set; }
		public int? SQ_PLANO_PREVIDENCIAL { get; set; }
		public int? SQ_CONTRATO_TRABALHO { get; set; }
		public int SQ_ESPECIE { get; set; }
		public int NR_PROCESSO { get; set; }
		public string NR_ANO_PROCESSO { get; set; }
		public int CD_PERFIL { get; set; }
		public int? SQ_MOT_SITUACAO { get; set; }
		public int NR_VERSAO { get; set; }
		public DateTime? DT_CONCESSAO { get; set; }
		public DateTime? DT_PROX_PAGTO { get; set; }
		public DateTime? DT_TERMINO { get; set; }
		public DateTime? DT_REINICIO { get; set; }
		public DateTime? DT_RETROATIVIDADE { get; set; }
		public int? QT_TOT_PARCELAS { get; set; }
		public int? QT_PAG_PARCELAS { get; set; }
		public decimal? VL_SALDO_INICIAL { get; set; }
		public decimal? VL_SALDO_ATUAL { get; set; }
		public decimal? VL_SALDO_ANTERIOR { get; set; }
		public decimal? PC_RESGATE { get; set; }
		public decimal? VL_RESGATE { get; set; }
		public decimal? VL_FATOR_REDUTOR { get; set; }
		public DateTime? DT_BASE_CALCULO { get; set; }
		public string IR_TIPO_CALCULO { get; set; }
		public string IR_EVOLUCAO_RENDA { get; set; }
		public string IR_CONVERSAO_RISCO { get; set; }
        
    }
}
