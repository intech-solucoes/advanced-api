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
    public abstract class NoticiaDAO : BaseDAO<NoticiaEntidade>
    {
        
		public virtual IEnumerable<NoticiaEntidade> Buscar()
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.Query<NoticiaEntidade>("SELECT *  FROM WEB_NOTICIA ORDER BY DTA_CRIACAO DESC", new {  });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.Query<NoticiaEntidade>("SELECT * FROM WEB_NOTICIA ORDER BY DTA_CRIACAO DESC", new {  });
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
