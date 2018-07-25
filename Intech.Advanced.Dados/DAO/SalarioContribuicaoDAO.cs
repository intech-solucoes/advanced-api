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
    public abstract class SalarioContribuicaoDAO : BaseDAO<SalarioContribuicaoEntidade>
    {
        
		public virtual IEnumerable<SalarioContribuicaoEntidade> BuscarPorContratoTrabalhoPlanoPeriodo(int SQ_CONTRATO_TRABALHO, int SQ_PLANO_PREVIDENCIAL, DateTime DT_INI, DateTime DT_FIM)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.Query<SalarioContribuicaoEntidade>("SELECT * FROM FI_FICHA_SALARIO_CONTRIBUICAO WHERE SQ_CONTRATO_TRABALHO = @SQ_CONTRATO_TRABALHO   AND SQ_PLANO_PREVIDENCIAL = @SQ_PLANO_PREVIDENCIAL   AND DT_REFERENCIA BETWEEN @DT_INI AND @DT_FIM ORDER BY DT_REFERENCIA DESC", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL, DT_INI, DT_FIM });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.Query<SalarioContribuicaoEntidade>("SELECT * FROM FI_FICHA_SALARIO_CONTRIBUICAO WHERE SQ_CONTRATO_TRABALHO=:SQ_CONTRATO_TRABALHO AND SQ_PLANO_PREVIDENCIAL=:SQ_PLANO_PREVIDENCIAL AND DT_REFERENCIA BETWEEN :DT_INI AND :DT_FIM ORDER BY DT_REFERENCIA DESC", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL, DT_INI, DT_FIM });
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				Conexao.Close();
			}
		}

		public virtual SalarioContribuicaoEntidade BuscarPorContratoTrabalhoPlanoReferencia(int SQ_CONTRATO_TRABALHO, int SQ_PLANO_PREVIDENCIAL, DateTime DT_REFERENCIA)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<SalarioContribuicaoEntidade>("SELECT * FROM FI_FICHA_SALARIO_CONTRIBUICAO WHERE SQ_CONTRATO_TRABALHO = @SQ_CONTRATO_TRABALHO   AND SQ_PLANO_PREVIDENCIAL = @SQ_PLANO_PREVIDENCIAL   AND DT_REFERENCIA = @DT_REFERENCIA", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL, DT_REFERENCIA });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<SalarioContribuicaoEntidade>("SELECT * FROM FI_FICHA_SALARIO_CONTRIBUICAO WHERE SQ_CONTRATO_TRABALHO=:SQ_CONTRATO_TRABALHO AND SQ_PLANO_PREVIDENCIAL=:SQ_PLANO_PREVIDENCIAL AND DT_REFERENCIA=:DT_REFERENCIA", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL, DT_REFERENCIA });
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				Conexao.Close();
			}
		}

		public virtual SalarioContribuicaoEntidade BuscarUltimoPorContratoTrabalhoPlano(int SQ_CONTRATO_TRABALHO, int SQ_PLANO_PREVIDENCIAL)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<SalarioContribuicaoEntidade>("SELECT TOP(1) *  FROM FI_FICHA_SALARIO_CONTRIBUICAO WHERE SQ_CONTRATO_TRABALHO = @SQ_CONTRATO_TRABALHO   AND SQ_PLANO_PREVIDENCIAL = @SQ_PLANO_PREVIDENCIAL ORDER BY DT_REFERENCIA DESC", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<SalarioContribuicaoEntidade>("SELECT * FROM FI_FICHA_SALARIO_CONTRIBUICAO WHERE SQ_CONTRATO_TRABALHO=:SQ_CONTRATO_TRABALHO AND SQ_PLANO_PREVIDENCIAL=:SQ_PLANO_PREVIDENCIAL AND ROWNUM <= 1  ORDER BY DT_REFERENCIA DESC", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL });
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
