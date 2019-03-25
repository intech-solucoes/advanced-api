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
    public abstract class ProcessoBeneficioDAO : BaseDAO<ProcessoBeneficioEntidade>
    {
        
		public virtual ProcessoBeneficioEntidade BuscarPorCdPessoa(int CD_PESSOA)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<ProcessoBeneficioEntidade>("SELECT * FROM FI_PROCESSO_BENEFICIO INNER JOIN FI_RECEBEDOR_BENEFICIO ON FI_RECEBEDOR_BENEFICIO.SQ_PROCESSO = FI_PROCESSO_BENEFICIO.SQ_PROCESSO WHERE FI_RECEBEDOR_BENEFICIO.CD_PESSOA_RECEB = @CD_PESSOA   AND FI_PROCESSO_BENEFICIO.SQ_MOT_SITUACAO = 4;", new { CD_PESSOA });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<ProcessoBeneficioEntidade>("SELECT * FROM FI_PROCESSO_BENEFICIO INNER  JOIN FI_RECEBEDOR_BENEFICIO  ON FI_RECEBEDOR_BENEFICIO.SQ_PROCESSO=FI_PROCESSO_BENEFICIO.SQ_PROCESSO WHERE FI_RECEBEDOR_BENEFICIO.CD_PESSOA_RECEB=:CD_PESSOA AND FI_PROCESSO_BENEFICIO.SQ_MOT_SITUACAO=4", new { CD_PESSOA });
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
