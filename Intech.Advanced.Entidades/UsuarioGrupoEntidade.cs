using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Intech.Advanced.Entidades
{
    [Table("FR_USUARIO_GRUPO")]
    public class UsuarioGrupoEntidade
    {
		public int GRP_CODIGO { get; set; }
		public string SIS_CODIGO { get; set; }
		public int USR_CODIGO { get; set; }
        
    }
}
