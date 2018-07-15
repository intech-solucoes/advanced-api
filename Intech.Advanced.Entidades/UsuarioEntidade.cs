using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
    [Table("FR_USUARIO")]
    public class UsuarioEntidade
    {
		[Key]
		public int USR_CODIGO { get; set; }
		public string USR_LOGIN { get; set; }
		public string USR_SENHA { get; set; }
		public string USR_ADMINISTRADOR { get; set; }
		public string USR_TIPO_EXPIRACAO { get; set; }
		public int? USR_DIAS_EXPIRACAO { get; set; }
		public string USR_IMAGEM_DIGITAL { get; set; }
		public string USR_FOTO { get; set; }
		public string USR_NOME { get; set; }
		public string USR_EMAIL { get; set; }
		public decimal? USR_DIGITAL { get; set; }
		public DateTime? USR_INICIO_EXPIRACAO { get; set; }
		public int? CD_PESSOA { get; set; }
		public string EE_TERMO_RESPONSABILIDADE { get; set; }
		public string USR_SENHA_NOVA { get; set; }
		public int? CD_PESSOA_CLIENTE { get; set; }
		public string NO_SENHA { get; set; }
        
    }
}
