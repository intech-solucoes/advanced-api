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
	public abstract class ContratoTrabalhoDAO : BaseDAO<ContratoTrabalhoEntidade>
	{
		public ContratoTrabalhoDAO (IDbTransaction tx = null) : base(tx) { }

		public virtual List<ContratoTrabalhoEntidade> BuscarMatriculasPorCpf(string CPF)
		{
			try
			{
				if (AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.Query<ContratoTrabalhoEntidade>("SELECT *  FROM FI_CONTRATO_TRABALHO CONTRATO INNER JOIN FI_PESSOA_FISICA ON FI_PESSOA_FISICA.CD_PESSOA = CONTRATO.CD_PESSOA INNER JOIN FI_PLANO_VINCULADO AS PLANO ON PLANO.SQ_CONTRATO_TRABALHO = CONTRATO.SQ_CONTRATO_TRABALHO WHERE FI_PESSOA_FISICA.NR_CPF = @CPF   AND PLANO.SQ_SIT_PLANO NOT IN (2, 6, 7)", new { CPF }).ToList();
				else if (AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.Query<ContratoTrabalhoEntidade>("SELECT * FROM FI_CONTRATO_TRABALHO  CONTRATO  INNER  JOIN FI_PESSOA_FISICA  ON FI_PESSOA_FISICA.CD_PESSOA=CONTRATO.CD_PESSOA INNER  JOIN FI_PLANO_VINCULADO PLANO  ON PLANO.SQ_CONTRATO_TRABALHO=CONTRATO.SQ_CONTRATO_TRABALHO WHERE FI_PESSOA_FISICA.NR_CPF=:CPF AND PLANO.SQ_SIT_PLANO NOT  IN (2, 6, 7)", new { CPF }).ToList();
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				if(Transaction == null)
					Conexao.Close();
			}
		}

		public virtual List<ContratoTrabalhoEntidade> BuscarPorCpfComPlanoAtivo(string CPF)
		{
			try
			{
				if (AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.Query<ContratoTrabalhoEntidade>("SELECT CT.*, 	EMPRESA.NO_PESSOA AS NO_EMPRESA FROM FI_CONTRATO_TRABALHO CT INNER JOIN FI_PESSOA_FISICA PF ON PF.CD_PESSOA = CT.CD_PESSOA INNER JOIN FI_PESSOA EMPRESA ON EMPRESA.CD_PESSOA = CT.CD_PESSOA_PATR INNER JOIN FI_PLANO_VINCULADO AS PLANO ON PLANO.SQ_CONTRATO_TRABALHO = CT.SQ_CONTRATO_TRABALHO WHERE PF.NR_CPF = @CPF   AND PLANO.SQ_SIT_PLANO NOT IN (2, 6, 7)", new { CPF }).ToList();
				else if (AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.Query<ContratoTrabalhoEntidade>("SELECT CT.*, EMPRESA.NO_PESSOA AS NO_EMPRESA FROM FI_CONTRATO_TRABALHO  CT  INNER  JOIN FI_PESSOA_FISICA   PF  ON PF.CD_PESSOA=CT.CD_PESSOA INNER  JOIN FI_PESSOA   EMPRESA  ON EMPRESA.CD_PESSOA=CT.CD_PESSOA_PATR INNER  JOIN FI_PLANO_VINCULADO PLANO  ON PLANO.SQ_CONTRATO_TRABALHO=CT.SQ_CONTRATO_TRABALHO WHERE PF.NR_CPF=:CPF AND PLANO.SQ_SIT_PLANO NOT  IN (2, 6, 7)", new { CPF }).ToList();
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