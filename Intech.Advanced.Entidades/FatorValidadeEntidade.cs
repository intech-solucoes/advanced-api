using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
    [Table("FI_FATOR_VALIDADE")]
    public class FatorValidadeEntidade
    {
		[Key]
		public int SQ_VALIDADE { get; set; }
		public int SQ_TIPO_TABUA { get; set; }
		public DateTime DT_VALIDADE { get; set; }
		public decimal VL_TX_JUROS { get; set; }
        
    }
}
