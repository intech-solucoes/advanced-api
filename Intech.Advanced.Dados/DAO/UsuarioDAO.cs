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
    public abstract class UsuarioDAO : BaseDAO<UsuarioEntidade>
    {
        
		public virtual UsuarioEntidade BuscarPorCPF(string CPF)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<UsuarioEntidade>("SELECT * FROM fr_usuario WHERE USR_LOGIN = @CPF", new { CPF });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<UsuarioEntidade>("SELECT * FROM FR_USUARIO WHERE USR_LOGIN=:CPF", new { CPF });
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				Conexao.Close();
			}
		}

		public virtual UsuarioEntidade BuscarPorLoginSenha(string USR_LOGIN, string USR_SENHA)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<UsuarioEntidade>("SELECT * FROM FR_USUARIO WHERE USR_LOGIN = @USR_LOGIN   AND USR_SENHA = @USR_SENHA", new { USR_LOGIN, USR_SENHA });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<UsuarioEntidade>("SELECT * FROM FR_USUARIO WHERE USR_LOGIN=:USR_LOGIN AND USR_SENHA=:USR_SENHA", new { USR_LOGIN, USR_SENHA });
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
