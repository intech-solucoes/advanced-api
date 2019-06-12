using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
    [Table("FI_TEMPO_SERVICO")]
    public class TempoServicoEntidade
    {
		[Key]
		public int SQ_TEMPO_SERVICO { get; set; }
		public int CD_PESSOA_EMPREGADO { get; set; }
		public int CD_PESSOA_EMPREGADOR { get; set; }
		public int? CD_GRUPO_ATIVIDADE { get; set; }
		public string IR_REG_ATIVIDADE { get; set; }
		public DateTime? DT_INIC_ATIVIDADE { get; set; }
		public DateTime? DT_TERM_ATIVIDADE { get; set; }
		public int? QT_ANOS { get; set; }
		public int? QT_MESES { get; set; }
		public int? QT_DIAS { get; set; }
		public string EE_COMPROVADO { get; set; }
		public string NR_MATRICULA { get; set; }
        
    }
}
