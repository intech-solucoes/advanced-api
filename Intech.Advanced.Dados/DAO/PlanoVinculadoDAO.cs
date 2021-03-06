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
    public abstract class PlanoVinculadoDAO : BaseDAO<PlanoVinculadoEntidade>
    {
        
		public virtual PlanoVinculadoEntidade BuscarPorContratoTrabalhoPlano(int SQ_CONTRATO_TRABALHO, int SQ_PLANO_PREVIDENCIAL)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<PlanoVinculadoEntidade>("SELECT      FI_PLANO_PREVIDENCIAL.DS_PLANO_PREVIDENCIAL,      FI_SIT_PLANO.DS_SIT_PLANO,      FI_SIT_INSCRICAO.DS_SIT_INSCRICAO,      FI_MOT_SIT_PLANO.DS_MOT_SIT_PLANO,      FI_PLANO_PREVIDENCIAL.NR_CODIGO_CNPB,      FI_PLANO_VINCULADO.DT_INSC_PLANO,     FI_PLANO_VINCULADO.DT_SITUACAO,     FI_PLANO_PREVIDENCIAL.CD_INDICE_VALORIZACAO,     FI_PLANO_VINCULADO.* FROM FI_PLANO_VINCULADO  INNER JOIN FI_PLANO_PREVIDENCIAL ON FI_PLANO_PREVIDENCIAL.SQ_PLANO_PREVIDENCIAL = FI_PLANO_VINCULADO.SQ_PLANO_PREVIDENCIAL INNER JOIN FI_SIT_PLANO ON FI_SIT_PLANO.SQ_SIT_PLANO = FI_PLANO_VINCULADO.SQ_SIT_PLANO INNER JOIN FI_MOT_SIT_PLANO ON FI_MOT_SIT_PLANO.SQ_MOT_SIT_PLANO = FI_PLANO_VINCULADO.SQ_MOT_SIT_PLANO INNER JOIN FI_SIT_INSCRICAO ON FI_SIT_INSCRICAO.SQ_SIT_INSCRICAO = FI_PLANO_VINCULADO.SQ_SIT_INSCRICAO WHERE SQ_CONTRATO_TRABALHO = @SQ_CONTRATO_TRABALHO   AND FI_PLANO_VINCULADO.SQ_SIT_PLANO NOT IN (2, 6)   AND FI_PLANO_PREVIDENCIAL.SQ_PLANO_PREVIDENCIAL = @SQ_PLANO_PREVIDENCIAL", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<PlanoVinculadoEntidade>("SELECT FI_PLANO_PREVIDENCIAL.DS_PLANO_PREVIDENCIAL, FI_SIT_PLANO.DS_SIT_PLANO, FI_SIT_INSCRICAO.DS_SIT_INSCRICAO, FI_MOT_SIT_PLANO.DS_MOT_SIT_PLANO, FI_PLANO_PREVIDENCIAL.NR_CODIGO_CNPB, FI_PLANO_VINCULADO.DT_INSC_PLANO, FI_PLANO_VINCULADO.DT_SITUACAO, FI_PLANO_PREVIDENCIAL.CD_INDICE_VALORIZACAO, FI_PLANO_VINCULADO.* FROM FI_PLANO_VINCULADO INNER  JOIN FI_PLANO_PREVIDENCIAL  ON FI_PLANO_PREVIDENCIAL.SQ_PLANO_PREVIDENCIAL=FI_PLANO_VINCULADO.SQ_PLANO_PREVIDENCIAL INNER  JOIN FI_SIT_PLANO  ON FI_SIT_PLANO.SQ_SIT_PLANO=FI_PLANO_VINCULADO.SQ_SIT_PLANO INNER  JOIN FI_MOT_SIT_PLANO  ON FI_MOT_SIT_PLANO.SQ_MOT_SIT_PLANO=FI_PLANO_VINCULADO.SQ_MOT_SIT_PLANO INNER  JOIN FI_SIT_INSCRICAO  ON FI_SIT_INSCRICAO.SQ_SIT_INSCRICAO=FI_PLANO_VINCULADO.SQ_SIT_INSCRICAO WHERE SQ_CONTRATO_TRABALHO=:SQ_CONTRATO_TRABALHO AND FI_PLANO_VINCULADO.SQ_SIT_PLANO NOT  IN (2, 6) AND FI_PLANO_PREVIDENCIAL.SQ_PLANO_PREVIDENCIAL=:SQ_PLANO_PREVIDENCIAL", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL });
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				Conexao.Close();
			}
		}

		public virtual IEnumerable<PlanoVinculadoEntidade> ListarPorContratoTrabalho(int SQ_CONTRATO_TRABALHO)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.Query<PlanoVinculadoEntidade>("SELECT      FI_PLANO_PREVIDENCIAL.DS_PLANO_PREVIDENCIAL,      FI_SIT_PLANO.DS_SIT_PLANO,      FI_SIT_INSCRICAO.DS_SIT_INSCRICAO,      FI_MOT_SIT_PLANO.DS_MOT_SIT_PLANO,      FI_PLANO_PREVIDENCIAL.NR_CODIGO_CNPB,      FI_PLANO_VINCULADO.DT_INSC_PLANO,     FI_PLANO_VINCULADO.DT_SITUACAO,     FI_PLANO_PREVIDENCIAL.CD_INDICE_VALORIZACAO,     FI_PLANO_VINCULADO.* FROM FI_PLANO_VINCULADO  INNER JOIN FI_PLANO_PREVIDENCIAL ON FI_PLANO_PREVIDENCIAL.SQ_PLANO_PREVIDENCIAL = FI_PLANO_VINCULADO.SQ_PLANO_PREVIDENCIAL INNER JOIN FI_SIT_PLANO ON FI_SIT_PLANO.SQ_SIT_PLANO = FI_PLANO_VINCULADO.SQ_SIT_PLANO INNER JOIN FI_MOT_SIT_PLANO ON FI_MOT_SIT_PLANO.SQ_MOT_SIT_PLANO = FI_PLANO_VINCULADO.SQ_MOT_SIT_PLANO INNER JOIN FI_SIT_INSCRICAO ON FI_SIT_INSCRICAO.SQ_SIT_INSCRICAO = FI_PLANO_VINCULADO.SQ_SIT_INSCRICAO WHERE SQ_CONTRATO_TRABALHO = @SQ_CONTRATO_TRABALHO   AND FI_PLANO_VINCULADO.SQ_SIT_PLANO NOT IN (2, 6)", new { SQ_CONTRATO_TRABALHO });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.Query<PlanoVinculadoEntidade>("SELECT FI_PLANO_PREVIDENCIAL.DS_PLANO_PREVIDENCIAL, FI_SIT_PLANO.DS_SIT_PLANO, FI_SIT_INSCRICAO.DS_SIT_INSCRICAO, FI_MOT_SIT_PLANO.DS_MOT_SIT_PLANO, FI_PLANO_PREVIDENCIAL.NR_CODIGO_CNPB, FI_PLANO_VINCULADO.DT_INSC_PLANO, FI_PLANO_VINCULADO.DT_SITUACAO, FI_PLANO_PREVIDENCIAL.CD_INDICE_VALORIZACAO, FI_PLANO_VINCULADO.* FROM FI_PLANO_VINCULADO INNER  JOIN FI_PLANO_PREVIDENCIAL  ON FI_PLANO_PREVIDENCIAL.SQ_PLANO_PREVIDENCIAL=FI_PLANO_VINCULADO.SQ_PLANO_PREVIDENCIAL INNER  JOIN FI_SIT_PLANO  ON FI_SIT_PLANO.SQ_SIT_PLANO=FI_PLANO_VINCULADO.SQ_SIT_PLANO INNER  JOIN FI_MOT_SIT_PLANO  ON FI_MOT_SIT_PLANO.SQ_MOT_SIT_PLANO=FI_PLANO_VINCULADO.SQ_MOT_SIT_PLANO INNER  JOIN FI_SIT_INSCRICAO  ON FI_SIT_INSCRICAO.SQ_SIT_INSCRICAO=FI_PLANO_VINCULADO.SQ_SIT_INSCRICAO WHERE SQ_CONTRATO_TRABALHO=:SQ_CONTRATO_TRABALHO AND FI_PLANO_VINCULADO.SQ_SIT_PLANO NOT  IN (2, 6)", new { SQ_CONTRATO_TRABALHO });
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				Conexao.Close();
			}
		}

		public virtual IEnumerable<PlanoVinculadoEntidade> ListarPorCpf(string CPF)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.Query<PlanoVinculadoEntidade>("SELECT      FI_PLANO_PREVIDENCIAL.DS_PLANO_PREVIDENCIAL,      FI_SIT_PLANO.DS_SIT_PLANO,      FI_SIT_INSCRICAO.DS_SIT_INSCRICAO,      FI_MOT_SIT_PLANO.DS_MOT_SIT_PLANO,      FI_PLANO_PREVIDENCIAL.NR_CODIGO_CNPB,      FI_PLANO_VINCULADO.DT_INSC_PLANO,     FI_PLANO_VINCULADO.DT_SITUACAO,     FI_PLANO_PREVIDENCIAL.CD_INDICE_VALORIZACAO,     FI_PLANO_VINCULADO.* FROM FI_PLANO_VINCULADO  INNER JOIN FI_PLANO_PREVIDENCIAL ON FI_PLANO_PREVIDENCIAL.SQ_PLANO_PREVIDENCIAL = FI_PLANO_VINCULADO.SQ_PLANO_PREVIDENCIAL INNER JOIN FI_SIT_PLANO ON FI_SIT_PLANO.SQ_SIT_PLANO = FI_PLANO_VINCULADO.SQ_SIT_PLANO INNER JOIN FI_MOT_SIT_PLANO ON FI_MOT_SIT_PLANO.SQ_MOT_SIT_PLANO = FI_PLANO_VINCULADO.SQ_MOT_SIT_PLANO INNER JOIN FI_SIT_INSCRICAO ON FI_SIT_INSCRICAO.SQ_SIT_INSCRICAO = FI_PLANO_VINCULADO.SQ_SIT_INSCRICAO INNER JOIN FI_CONTRATO_TRABALHO ON FI_CONTRATO_TRABALHO.SQ_CONTRATO_TRABALHO = FI_PLANO_VINCULADO.SQ_CONTRATO_TRABALHO INNER JOIN FI_PESSOA ON FI_PESSOA.CD_PESSOA = FI_CONTRATO_TRABALHO.CD_PESSOA INNER JOIN FI_PESSOA_FISICA ON FI_PESSOA_FISICA.CD_PESSOA = FI_CONTRATO_TRABALHO.CD_PESSOA WHERE FI_PESSOA_FISICA.NR_CPF = @CPF   AND FI_PLANO_VINCULADO.SQ_SIT_PLANO NOT IN (2, 6) ORDER BY FI_PLANO_VINCULADO.DT_INSC_PLANO DESC", new { CPF });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.Query<PlanoVinculadoEntidade>("SELECT FI_PLANO_PREVIDENCIAL.DS_PLANO_PREVIDENCIAL, FI_SIT_PLANO.DS_SIT_PLANO, FI_SIT_INSCRICAO.DS_SIT_INSCRICAO, FI_MOT_SIT_PLANO.DS_MOT_SIT_PLANO, FI_PLANO_PREVIDENCIAL.NR_CODIGO_CNPB, FI_PLANO_VINCULADO.DT_INSC_PLANO, FI_PLANO_VINCULADO.DT_SITUACAO, FI_PLANO_PREVIDENCIAL.CD_INDICE_VALORIZACAO, FI_PLANO_VINCULADO.* FROM FI_PLANO_VINCULADO INNER  JOIN FI_PLANO_PREVIDENCIAL  ON FI_PLANO_PREVIDENCIAL.SQ_PLANO_PREVIDENCIAL=FI_PLANO_VINCULADO.SQ_PLANO_PREVIDENCIAL INNER  JOIN FI_SIT_PLANO  ON FI_SIT_PLANO.SQ_SIT_PLANO=FI_PLANO_VINCULADO.SQ_SIT_PLANO INNER  JOIN FI_MOT_SIT_PLANO  ON FI_MOT_SIT_PLANO.SQ_MOT_SIT_PLANO=FI_PLANO_VINCULADO.SQ_MOT_SIT_PLANO INNER  JOIN FI_SIT_INSCRICAO  ON FI_SIT_INSCRICAO.SQ_SIT_INSCRICAO=FI_PLANO_VINCULADO.SQ_SIT_INSCRICAO INNER  JOIN FI_CONTRATO_TRABALHO  ON FI_CONTRATO_TRABALHO.SQ_CONTRATO_TRABALHO=FI_PLANO_VINCULADO.SQ_CONTRATO_TRABALHO INNER  JOIN FI_PESSOA  ON FI_PESSOA.CD_PESSOA=FI_CONTRATO_TRABALHO.CD_PESSOA INNER  JOIN FI_PESSOA_FISICA  ON FI_PESSOA_FISICA.CD_PESSOA=FI_CONTRATO_TRABALHO.CD_PESSOA WHERE FI_PESSOA_FISICA.NR_CPF=:CPF AND FI_PLANO_VINCULADO.SQ_SIT_PLANO NOT  IN (2, 6) ORDER BY FI_PLANO_VINCULADO.DT_INSC_PLANO DESC", new { CPF });
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
