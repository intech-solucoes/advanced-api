using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
    [Table("WEB_NOTICIA")]
    public class NoticiaEntidade
    {
		[Key]
		public decimal OID_NOTICIA { get; set; }
		public string TXT_TITULO { get; set; }
		public DateTime DTA_CRIACAO { get; set; }
		public string TXT_RESUMO { get; set; }
		public string TXT_LINK { get; set; }
		public string TXT_CONTEUDO { get; set; }
        
    }
}
