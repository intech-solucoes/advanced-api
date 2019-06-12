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
    public abstract class TempoServicoDAO : BaseDAO<TempoServicoEntidade>
    {
        
		public virtual IEnumerable<TempoServicoEntidade> BuscarPorCdPessoa(int CD_PESSOA_EMPREGADO)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.Query<TempoServicoEntidade>("SELECT * FROM FI_TEMPO_SERVICO  WHERE CD_PESSOA_EMPREGADO = @CD_PESSOA_EMPREGADO ORDER BY DT_INIC_ATIVIDADE", new { CD_PESSOA_EMPREGADO });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.Query<TempoServicoEntidade>("SELECT * FROM FI_TEMPO_SERVICO WHERE CD_PESSOA_EMPREGADO=:CD_PESSOA_EMPREGADO ORDER BY DT_INIC_ATIVIDADE", new { CD_PESSOA_EMPREGADO });
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
