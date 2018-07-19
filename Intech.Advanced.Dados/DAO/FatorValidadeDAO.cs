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
    public abstract class FatorValidadeDAO : BaseDAO<FatorValidadeEntidade>
    {
        
		public virtual FatorValidadeEntidade BuscarUltimo()
		{
			if(AppSettings.IS_SQL_SERVER_PROVIDER)
				return Conexao.QuerySingleOrDefault<FatorValidadeEntidade>("SELECT TOP 1 * FROM FI_FATOR_VALIDADE ORDER BY DT_VALIDADE DESC", new {  });
			else if(AppSettings.IS_ORACLE_PROVIDER)
				return Conexao.QuerySingleOrDefault<FatorValidadeEntidade>("SELECT * FROM FI_FATOR_VALIDADE WHERE ROWNUM <= 1  ORDER BY DT_VALIDADE DESC", new {  });
			else
				throw new Exception("Provider não suportado!");
		}
    }
}
