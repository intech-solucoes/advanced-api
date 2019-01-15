#region Usings
using Dapper;
using Intech.Lib.Dapper;
using Intech.Lib.Web;
using Intech.Advanced.Entidades;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
#endregion

namespace Intech.Advanced.Dados.DAO
{   
    public abstract class UsuarioGrupoDAO : BaseDAO<UsuarioGrupoEntidade>
    {
        
		public virtual UsuarioGrupoEntidade BuscarPorUsuario(int USR_CODIGO)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<UsuarioGrupoEntidade>("SELECT * FROM FR_USUARIO_GRUPO WHERE USR_CODIGO = @USR_CODIGO", new { USR_CODIGO });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<UsuarioGrupoEntidade>("SELECT * FROM FR_USUARIO_GRUPO WHERE USR_CODIGO=:USR_CODIGO", new { USR_CODIGO });
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				Conexao.Close();
			}
		}

    }
}
