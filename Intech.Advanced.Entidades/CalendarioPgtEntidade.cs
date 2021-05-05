using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
	[Table("WEB_CALENDARIO_PGT")]
	public class CalendarioPgtEntidade
	{
		[Key]
		public decimal OID_CALENDARIO_PGT { get; set; }
		public string DES_MES { get; set; }
		public decimal NUM_DIA { get; set; }
		public string CD_PLANO { get; set; }
	}
}