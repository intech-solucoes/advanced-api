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
    public abstract class ContrachequeDAO : BaseDAO<ContrachequeEntidade>
    {
        
		public virtual IEnumerable<ContrachequeEntidade> BuscarDatasPorPlanoContratoTrabalho(int SQ_CONTRATO_TRABALHO, int SQ_PLANO_PREVIDENCIAL)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.Query<ContrachequeEntidade>("SELECT DISTINCT  	CRONO.DT_REFERENCIA,     CRONO.SQ_CRONOGRAMA FROM FI_FICHA_FINANC_ASSISTIDO FF INNER JOIN FI_PROCESSO_BENEFICIO PB ON PB.SQ_PROCESSO = FF.SQ_PROCESSO INNER JOIN FI_CRONOGRAMA_CREDITO CRONO ON CRONO.SQ_CRONOGRAMA = FF.SQ_CRONOGRAMA WHERE PB.SQ_CONTRATO_TRABALHO = @SQ_CONTRATO_TRABALHO   AND PB.SQ_PLANO_PREVIDENCIAL = @SQ_PLANO_PREVIDENCIAL ORDER BY CRONO.DT_REFERENCIA DESC", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.Query<ContrachequeEntidade>("SELECT DISTINCT CRONO.DT_REFERENCIA, CRONO.SQ_CRONOGRAMA FROM FI_FICHA_FINANC_ASSISTIDO  FF  INNER  JOIN FI_PROCESSO_BENEFICIO   PB  ON PB.SQ_PROCESSO=FF.SQ_PROCESSO INNER  JOIN FI_CRONOGRAMA_CREDITO   CRONO  ON CRONO.SQ_CRONOGRAMA=FF.SQ_CRONOGRAMA WHERE PB.SQ_CONTRATO_TRABALHO=:SQ_CONTRATO_TRABALHO AND PB.SQ_PLANO_PREVIDENCIAL=:SQ_PLANO_PREVIDENCIAL ORDER BY CRONO.DT_REFERENCIA DESC", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL });
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				Conexao.Close();
			}
		}

		public virtual IEnumerable<ContrachequeEntidade> BuscarPorPlanoContratoTrabalho(int SQ_CONTRATO_TRABALHO, int SQ_PLANO_PREVIDENCIAL)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.Query<ContrachequeEntidade>("SELECT  	RUBRICA.DS_RUBRICA, 	CRONO.DT_REFERENCIA, 	FF.* FROM FI_FICHA_FINANC_ASSISTIDO FF INNER JOIN FI_PROCESSO_BENEFICIO PB ON PB.SQ_PROCESSO = FF.SQ_PROCESSO INNER JOIN FI_CRONOGRAMA_CREDITO CRONO ON CRONO.SQ_CRONOGRAMA = FF.SQ_CRONOGRAMA INNER JOIN FI_RUBRICA_FOLHA_PAGAMENTO RUBRICA ON RUBRICA.SQ_RUBRICA = FF.SQ_RUBRICA WHERE PB.SQ_CONTRATO_TRABALHO = @SQ_CONTRATO_TRABALHO   AND PB.SQ_PLANO_PREVIDENCIAL = @SQ_PLANO_PREVIDENCIAL", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.Query<ContrachequeEntidade>("SELECT RUBRICA.DS_RUBRICA, CRONO.DT_REFERENCIA, FF.* FROM FI_FICHA_FINANC_ASSISTIDO  FF  INNER  JOIN FI_PROCESSO_BENEFICIO   PB  ON PB.SQ_PROCESSO=FF.SQ_PROCESSO INNER  JOIN FI_CRONOGRAMA_CREDITO   CRONO  ON CRONO.SQ_CRONOGRAMA=FF.SQ_CRONOGRAMA INNER  JOIN FI_RUBRICA_FOLHA_PAGAMENTO   RUBRICA  ON RUBRICA.SQ_RUBRICA=FF.SQ_RUBRICA WHERE PB.SQ_CONTRATO_TRABALHO=:SQ_CONTRATO_TRABALHO AND PB.SQ_PLANO_PREVIDENCIAL=:SQ_PLANO_PREVIDENCIAL", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL });
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				Conexao.Close();
			}
		}

		public virtual IEnumerable<ContrachequeEntidade> BuscarPorPlanoContratoTrabalhoCronograma(int SQ_CONTRATO_TRABALHO, int SQ_PLANO_PREVIDENCIAL, int SQ_CRONOGRAMA)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.Query<ContrachequeEntidade>("SELECT DISTINCT  	CRONO.DT_REFERENCIA, 	RUBRICA.DS_RUBRICA,     FF.* FROM FI_FICHA_FINANC_ASSISTIDO FF INNER JOIN FI_PROCESSO_BENEFICIO PB ON PB.SQ_PROCESSO = FF.SQ_PROCESSO INNER JOIN FI_CRONOGRAMA_CREDITO CRONO ON CRONO.SQ_CRONOGRAMA = FF.SQ_CRONOGRAMA INNER JOIN FI_RUBRICA_FOLHA_PAGAMENTO RUBRICA ON RUBRICA.SQ_RUBRICA = FF.SQ_RUBRICA WHERE PB.SQ_CONTRATO_TRABALHO = @SQ_CONTRATO_TRABALHO   AND PB.SQ_PLANO_PREVIDENCIAL = @SQ_PLANO_PREVIDENCIAL   AND CRONO.SQ_CRONOGRAMA = @SQ_CRONOGRAMA   AND FF.IR_LANCAMENTO <> 'L'", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL, SQ_CRONOGRAMA });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.Query<ContrachequeEntidade>("SELECT DISTINCT CRONO.DT_REFERENCIA, RUBRICA.DS_RUBRICA, FF.* FROM FI_FICHA_FINANC_ASSISTIDO  FF  INNER  JOIN FI_PROCESSO_BENEFICIO   PB  ON PB.SQ_PROCESSO=FF.SQ_PROCESSO INNER  JOIN FI_CRONOGRAMA_CREDITO   CRONO  ON CRONO.SQ_CRONOGRAMA=FF.SQ_CRONOGRAMA INNER  JOIN FI_RUBRICA_FOLHA_PAGAMENTO   RUBRICA  ON RUBRICA.SQ_RUBRICA=FF.SQ_RUBRICA WHERE PB.SQ_CONTRATO_TRABALHO=:SQ_CONTRATO_TRABALHO AND PB.SQ_PLANO_PREVIDENCIAL=:SQ_PLANO_PREVIDENCIAL AND CRONO.SQ_CRONOGRAMA=:SQ_CRONOGRAMA AND FF.IR_LANCAMENTO<>'L'", new { SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL, SQ_CRONOGRAMA });
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
