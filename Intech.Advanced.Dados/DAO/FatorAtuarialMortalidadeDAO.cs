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
        
		public virtual FatorAtuarialMortalidadeEntidade BuscarPorIdadePartIdadeDepSexo(int IDADE_PART, int IDADE_DEP, string SEXO)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<FatorAtuarialMortalidadeEntidade>("SELECT * FROM  FI_FATOR_ATUARIAL_MORTALIDADE WHERE (FI_FATOR_ATUARIAL_MORTALIDADE.IC_TABELA = 'AXY')    AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_TIT = @IDADE_PART)    AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_DEP = @IDADE_DEP)   AND (FI_FATOR_ATUARIAL_MORTALIDADE.IR_SEXO = @SEXO)", new { IDADE_PART, IDADE_DEP, SEXO });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<FatorAtuarialMortalidadeEntidade>("SELECT * FROM FI_FATOR_ATUARIAL_MORTALIDADE WHERE (FI_FATOR_ATUARIAL_MORTALIDADE.IC_TABELA='AXY') AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_TIT=:IDADE_PART) AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_DEP=:IDADE_DEP) AND (FI_FATOR_ATUARIAL_MORTALIDADE.IR_SEXO=:SEXO)", new { IDADE_PART, IDADE_DEP, SEXO });
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				Conexao.Close();
			}
		}

		public virtual FatorAtuarialMortalidadeEntidade BuscarPorIdadeSexo(int IDADE, string SEXO)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<FatorAtuarialMortalidadeEntidade>("SELECT  * FROM  FI_FATOR_ATUARIAL_MORTALIDADE WHERE (FI_FATOR_ATUARIAL_MORTALIDADE.IC_TABELA = 'AX')    AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_TIT = @IDADE)   AND (FI_FATOR_ATUARIAL_MORTALIDADE.IR_SEXO = @SEXO)", new { IDADE, SEXO });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<FatorAtuarialMortalidadeEntidade>("SELECT * FROM FI_FATOR_ATUARIAL_MORTALIDADE WHERE (FI_FATOR_ATUARIAL_MORTALIDADE.IC_TABELA='AX') AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_TIT=:IDADE) AND (FI_FATOR_ATUARIAL_MORTALIDADE.IR_SEXO=:SEXO)", new { IDADE, SEXO });
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				Conexao.Close();
			}
		}

		public virtual FatorAtuarialMortalidadeEntidade BuscarPorTabelaIdadeSexo(string IC_TABELA, int IDADE, string SEXO)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<FatorAtuarialMortalidadeEntidade>("SELECT  * FROM  FI_FATOR_ATUARIAL_MORTALIDADE WHERE (FI_FATOR_ATUARIAL_MORTALIDADE.IC_TABELA = @IC_TABELA)    AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_TIT = @IDADE)   AND (FI_FATOR_ATUARIAL_MORTALIDADE.IR_SEXO = @SEXO)", new { IC_TABELA, IDADE, SEXO });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<FatorAtuarialMortalidadeEntidade>("SELECT * FROM FI_FATOR_ATUARIAL_MORTALIDADE WHERE (FI_FATOR_ATUARIAL_MORTALIDADE.IC_TABELA=:IC_TABELA) AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_TIT=:IDADE) AND (FI_FATOR_ATUARIAL_MORTALIDADE.IR_SEXO=:SEXO)", new { IC_TABELA, IDADE, SEXO });
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
