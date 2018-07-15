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
    public abstract class IndiceDAO : BaseDAO<IndiceEntidade>
    {
        
		public virtual IEnumerable<IndiceEntidade> BuscarPorCdIndice(string CD_INDICE)
		{
			if(AppSettings.IS_SQL_SERVER_PROVIDER)
				return Conexao.Query<IndiceEntidade>("SELECT *  FROM FI_INDICE_VALORES WHERE CD_INDICE = @CD_INDICE   AND EE_REAL = 'S' ORDER BY DT_INIC_VALIDADE DESC", new { CD_INDICE });
			else if(AppSettings.IS_ORACLE_PROVIDER)
				return Conexao.Query<IndiceEntidade>("SELECT * FROM FI_INDICE_VALORES WHERE CD_INDICE=:CD_INDICE AND EE_REAL='S' ORDER BY DT_INIC_VALIDADE DESC", new { CD_INDICE });
			else
				throw new Exception("Provider não suportado!");
		}
		public virtual IEnumerable<IndiceEntidade> BuscarPorCdIndicePeriodo(string CD_INDICE, DateTime DT_INI, DateTime DT_FIM)
		{
			if(AppSettings.IS_SQL_SERVER_PROVIDER)
				return Conexao.Query<IndiceEntidade>("SELECT *  FROM FI_INDICE_VALORES WHERE CD_INDICE = @CD_INDICE   and DT_INIC_VALIDADE >= @DT_INI   and DT_INIC_VALIDADE <= @DT_FIM ORDER BY DT_INIC_VALIDADE DESC", new { CD_INDICE, DT_INI, DT_FIM });
			else if(AppSettings.IS_ORACLE_PROVIDER)
				return Conexao.Query<IndiceEntidade>("SELECT * FROM FI_INDICE_VALORES WHERE CD_INDICE=:CD_INDICE AND DT_INIC_VALIDADE>=:DT_INI AND DT_INIC_VALIDADE<=:DT_FIM ORDER BY DT_INIC_VALIDADE DESC", new { CD_INDICE, DT_INI, DT_FIM });
			else
				throw new Exception("Provider não suportado!");
		}
		public virtual IndiceEntidade BuscarUltimoPorCdIndice(string CD_INDICE)
		{
			if(AppSettings.IS_SQL_SERVER_PROVIDER)
				return Conexao.QuerySingleOrDefault<IndiceEntidade>("SELECT TOP 1 *  FROM FI_INDICE_VALORES WHERE CD_INDICE = @CD_INDICE ORDER BY DT_INIC_VALIDADE DESC", new { CD_INDICE });
			else if(AppSettings.IS_ORACLE_PROVIDER)
				return Conexao.QuerySingleOrDefault<IndiceEntidade>("SELECT * FROM FI_INDICE_VALORES WHERE CD_INDICE=:CD_INDICE AND ROWNUM <= 1  ORDER BY DT_INIC_VALIDADE DESC", new { CD_INDICE });
			else
				throw new Exception("Provider não suportado!");
		}
    }
}
