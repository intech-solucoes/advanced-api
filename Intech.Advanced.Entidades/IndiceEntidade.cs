using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
    [Table("FI_INDICE_VALORES")]
    public class IndiceEntidade
    {
		public string CD_INDICE { get; set; }
		public DateTime DT_INIC_VALIDADE { get; set; }
		public decimal? VL_INDICE { get; set; }
		public decimal? VL_VARIACAO { get; set; }
		public string EE_REAL { get; set; }
        
    }
}
