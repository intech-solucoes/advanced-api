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
    public abstract class DependenteDAO : BaseDAO<DependenteEntidade>
    {
        
		public virtual DependenteEntidade BuscarDependentePorContratoTrabalhoDtValidadeTipo(int SQ_CONTRATO_TRABALHO, string TIPO, DateTime DT_VALIDADE)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<DependenteEntidade>("SELECT TOP 1 C.* FROM FI_DEPENDENTE A   INNER JOIN  FI_CONTRATO_TRABALHO B ON B.CD_PESSOA = A.CD_PESSOA_PAI   INNER JOIN  FI_HIST_DEPENDENTE_PREVIDENCIAL X ON (X.CD_PESSOA = A.CD_PESSOA_PAI)     AND (X.CD_PESSOA_DEP = A.CD_PESSOA_DEP)   INNER JOIN FI_PESSOA_FISICA C ON C.CD_PESSOA = A.CD_PESSOA_DEP   INNER JOIN FI_GRAU_PARENTESCO D ON (D.CD_GRAU_PARENTESCO = A.CD_GRAU_PARENTESCO)      AND (D.IR_TIPO_VALIDADE = @TIPO) WHERE (B.SQ_CONTRATO_TRABALHO = @SQ_CONTRATO_TRABALHO)    AND (X.SQ_PLANO_PREVIDENCIAL = 3)    AND (A.DT_TERM_VALIDADE >= @DT_VALIDADE) ORDER BY C.DT_NASCIMENTO DESC", new { SQ_CONTRATO_TRABALHO, TIPO, DT_VALIDADE });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<DependenteEntidade>("SELECT C.* FROM FI_DEPENDENTE  A  INNER  JOIN FI_CONTRATO_TRABALHO   B  ON B.CD_PESSOA=A.CD_PESSOA_PAI INNER  JOIN FI_HIST_DEPENDENTE_PREVIDENCIAL   X  ON (X.CD_PESSOA=A.CD_PESSOA_PAI) AND (X.CD_PESSOA_DEP=A.CD_PESSOA_DEP) INNER  JOIN FI_PESSOA_FISICA   C  ON C.CD_PESSOA=A.CD_PESSOA_DEP INNER  JOIN FI_GRAU_PARENTESCO   D  ON (D.CD_GRAU_PARENTESCO=A.CD_GRAU_PARENTESCO) AND (D.IR_TIPO_VALIDADE=:TIPO) WHERE (B.SQ_CONTRATO_TRABALHO=:SQ_CONTRATO_TRABALHO) AND (X.SQ_PLANO_PREVIDENCIAL=3) AND (A.DT_TERM_VALIDADE>=:DT_VALIDADE) AND ROWNUM <= 1  ORDER BY C.DT_NASCIMENTO DESC", new { SQ_CONTRATO_TRABALHO, TIPO, DT_VALIDADE });
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
