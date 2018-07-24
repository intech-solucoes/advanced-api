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
    public abstract class HistManutContribuicaoDAO : BaseDAO<HistManutContribuicaoEntidade>
    {
        
		public virtual HistManutContribuicaoEntidade BuscarUltimoPorContratoTrabalho(int SQ_CONTRATO_TRABALHO)
		{
			if(AppSettings.IS_SQL_SERVER_PROVIDER)
				return Conexao.QuerySingleOrDefault<HistManutContribuicaoEntidade>("SELECT TOP 1 * FROM FI_HIST_MANUT_CONTRIBUICAO WHERE SQ_CONTRATO_TRABALHO = @SQ_CONTRATO_TRABALHO   AND SQ_REGRA = 3   AND SQ_PLANO_PREVIDENCIAL = 3 ORDER BY DT_INIC_VALIDADE DESC", new { SQ_CONTRATO_TRABALHO });
			else if(AppSettings.IS_ORACLE_PROVIDER)
				return Conexao.QuerySingleOrDefault<HistManutContribuicaoEntidade>("SELECT * FROM FI_HIST_MANUT_CONTRIBUICAO WHERE SQ_CONTRATO_TRABALHO=:SQ_CONTRATO_TRABALHO AND SQ_REGRA=3 AND SQ_PLANO_PREVIDENCIAL=3 AND ROWNUM <= 1  ORDER BY DT_INIC_VALIDADE DESC", new { SQ_CONTRATO_TRABALHO });
			else
				throw new Exception("Provider não suportado!");
		}
    }
}
