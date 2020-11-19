using Dapper;
using Intech.Lib.Dapper;
using Intech.Lib.Web;
using Intech.Advanced.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Intech.Advanced.Dados.DAO
{
	public abstract class HistValoresProcessoDAO : BaseDAO<HistValoresProcessoEntidade>
	{
		public HistValoresProcessoDAO (IDbTransaction tx = null) : base(tx) { }

		public virtual HistValoresProcessoEntidade BuscarPorCpfPlano(string CPF, int SQ_PLANO_PREVIDENCIAL)
		{
			try
			{
				if (AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<HistValoresProcessoEntidade>("SELECT HP.*  FROM FI_HIST_VALORES_PROCESSO HP       INNER JOIN FI_PROCESSO_BENEFICIO PB ON HP.SQ_PROCESSO = PB.SQ_PROCESSO      INNER JOIN FI_CONTRATO_TRABALHO CT ON CT.SQ_CONTRATO_TRABALHO = PB.SQ_CONTRATO_TRABALHO      INNER JOIN FI_PESSOA_FISICA PF ON PF.CD_PESSOA = CT.CD_PESSOA  WHERE PF.NR_CPF = @CPF    AND PB.SQ_PLANO_PREVIDENCIAL = @SQ_PLANO_PREVIDENCIAL    AND HP.DT_INIC_VALIDADE = (SELECT MAX(HP2.DT_INIC_VALIDADE)                                 FROM FI_HIST_VALORES_PROCESSO HP2                                WHERE HP2.SQ_PROCESSO = PB.SQ_PROCESSO)", new { CPF, SQ_PLANO_PREVIDENCIAL });
				else if (AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<HistValoresProcessoEntidade>("SELECT HP.* FROM FI_HIST_VALORES_PROCESSO  HP  INNER  JOIN FI_PROCESSO_BENEFICIO   PB  ON HP.SQ_PROCESSO=PB.SQ_PROCESSO INNER  JOIN FI_CONTRATO_TRABALHO   CT  ON CT.SQ_CONTRATO_TRABALHO=PB.SQ_CONTRATO_TRABALHO INNER  JOIN FI_PESSOA_FISICA   PF  ON PF.CD_PESSOA=CT.CD_PESSOA WHERE PF.NR_CPF=:CPF AND PB.SQ_PLANO_PREVIDENCIAL=:SQ_PLANO_PREVIDENCIAL AND HP.DT_INIC_VALIDADE=(SELECT MAX(HP2.DT_INIC_VALIDADE) FROM FI_HIST_VALORES_PROCESSO  HP2  WHERE HP2.SQ_PROCESSO=PB.SQ_PROCESSO)", new { CPF, SQ_PLANO_PREVIDENCIAL });
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				if(Transaction == null)
					Conexao.Close();
			}
		}

	}
}
