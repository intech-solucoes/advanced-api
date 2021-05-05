using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
	[Table("FI_FICHA_FINANC_ASSISTIDO")]
	public class ContrachequeEntidade
	{
		[Key]
		public int SQ_FICHA { get; set; }
		public int SQ_PROCESSO { get; set; }
		public int CD_PESSOA_RECEB { get; set; }
		public int? SQ_RUBRICA { get; set; }
		public int? SQ_CRONOGRAMA { get; set; }
		public DateTime DT_COMPETENCIA { get; set; }
		public decimal? VL_CALCULO { get; set; }
		public decimal? VL_COTAS { get; set; }
		public decimal? VL_BASE_CALCULO { get; set; }
		public int? QT_PRAZO { get; set; }
		public int? NR_PARCELA { get; set; }
		public int? QT_PARCELA { get; set; }
		public string IR_LANCAMENTO { get; set; }
		public string DS_REFR { get; set; }
		[Write(false)] public DateTime? DT_REFERENCIA { get; set; }
		[Write(false)] public string DS_RUBRICA { get; set; }
	}
}