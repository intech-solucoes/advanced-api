using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
    [Table("FI_DEPENDENTE")]
    public class DependenteEntidade
    {
		[Key]
		public int SQ_PESSOA_DEP { get; set; }
		public int CD_PESSOA_DEP { get; set; }
		public int CD_PESSOA_PAI { get; set; }
		public DateTime? DT_INCLUSAO_DEP { get; set; }
		public DateTime? DT_TERM_VALIDADE { get; set; }
		public int? CD_MOT_PERDA_VALIDADE { get; set; }
		public int CD_GRAU_PARENTESCO { get; set; }
		public string IR_ASSISTENCIAL { get; set; }
		public string IR_PREVIDENCIAL { get; set; }
		public string IR_ABATIMENTO_IRRF { get; set; }
		public DateTime? DT_INIC_IRRF { get; set; }
		public DateTime? DT_TERM_IRRF { get; set; }
		public int? SQ_TIPO_DEPENDENTE { get; set; }
		public string EE_SALARIO_FAMILIA { get; set; }
		public string EE_DESIGNADO { get; set; }
        
    }
}
