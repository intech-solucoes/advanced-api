using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
    [Table("FI_FICHA_SALARIO_CONTRIBUICAO")]
    public class SalarioContribuicaoEntidade
    {
		[Key]
		public int SQ_FICHA { get; set; }
		public int SQ_PLANO_PREVIDENCIAL { get; set; }
		public int SQ_CONTRATO_TRABALHO { get; set; }
		public DateTime DT_REFERENCIA { get; set; }
		public decimal? VL_BASE_FUNDACAO { get; set; }
		public decimal? VL_BASE_PREVIDENCIA { get; set; }
        
    }
}
