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
    public abstract class FatorAtuarialMortalidadeDAO : BaseDAO<FatorAtuarialMortalidadeEntidade>
    {
        
		public virtual FatorAtuarialMortalidadeEntidade BuscarPorIdade(int IDADE)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<FatorAtuarialMortalidadeEntidade>("SELECT  FI_FATOR_ATUARIAL_MORTALIDADE.VL_FATOR_A FROM  FI_FATOR_ATUARIAL_MORTALIDADE WHERE  (FI_FATOR_ATUARIAL_MORTALIDADE.IC_TABELA = 'AX') AND   (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_TIT = @IDADE)", new { IDADE });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<FatorAtuarialMortalidadeEntidade>("SELECT FI_FATOR_ATUARIAL_MORTALIDADE.VL_FATOR_A FROM FI_FATOR_ATUARIAL_MORTALIDADE WHERE (FI_FATOR_ATUARIAL_MORTALIDADE.IC_TABELA='AX') AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_TIT=:IDADE)", new { IDADE });
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				Conexao.Close();
			}
		}

		public virtual FatorAtuarialMortalidadeEntidade BuscarPorIdadePartIdadeDep(int IDADE_PART, int IDADE_DEP)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<FatorAtuarialMortalidadeEntidade>("SELECT  FI_FATOR_ATUARIAL_MORTALIDADE.VL_FATOR_A FROM  FI_FATOR_ATUARIAL_MORTALIDADE WHERE (FI_FATOR_ATUARIAL_MORTALIDADE.IC_TABELA = 'AX')    AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_TIT = @IDADE_PART)    AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_DEP = @IDADE_DEP)", new { IDADE_PART, IDADE_DEP });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<FatorAtuarialMortalidadeEntidade>("SELECT FI_FATOR_ATUARIAL_MORTALIDADE.VL_FATOR_A FROM FI_FATOR_ATUARIAL_MORTALIDADE WHERE (FI_FATOR_ATUARIAL_MORTALIDADE.IC_TABELA='AX') AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_TIT=:IDADE_PART) AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_DEP=:IDADE_DEP)", new { IDADE_PART, IDADE_DEP });
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
