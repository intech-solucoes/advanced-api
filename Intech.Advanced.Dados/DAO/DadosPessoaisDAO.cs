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
    public abstract class DadosPessoaisDAO : BaseDAO<DadosPessoaisEntidade>
    {
        
		public virtual DadosPessoaisEntidade BuscarPorCdPessoa(int CD_PESSOA)
		{
			if(AppSettings.IS_SQL_SERVER_PROVIDER)
				return Conexao.QuerySingleOrDefault<DadosPessoaisEntidade>("SELECT      CONTRATO.SQ_CONTRATO_TRABALHO,      CONTRATO.NR_REGISTRO,      CONTRATO.DT_ADMISSAO,      CONTRATO.DT_DEMISSAO,      FI_PESSOA.NO_PESSOA,      FI_PESSOA_FISICA.IR_SEXO,      FI_PESSOA_FISICA.DT_NASCIMENTO,      FI_PESSOA_FISICA.NR_CPF,     FI_PESSOA.CD_PESSOA,     EMPRESA.NO_SIGLA AS SIGLA_EMPRESA, 	CASE 		WHEN FI_PESSOA_FISICA.IR_SEXO = 'M' THEN 'MASCULINO' 		WHEN FI_PESSOA_FISICA.IR_SEXO = 'F' THEN 'FEMININO' 	END AS DS_SEXO, 	ENDERECO.NR_CEP, 	ENDERECO.DS_ENDERECO, 	ENDERECO.NR_ENDERECO, 	ENDERECO.DS_COMPLEMENTO, 	ENDERECO.NO_BAIRRO, 	ENDERECO.NR_FONE, 	ENDERECO.NR_CELULAR, 	ENDERECO.NO_EMAIL FROM FI_CONTRATO_TRABALHO AS CONTRATO INNER JOIN FI_PESSOA ON FI_PESSOA.CD_PESSOA = CONTRATO.CD_PESSOA INNER JOIN FI_PESSOA_FISICA ON FI_PESSOA_FISICA.CD_PESSOA = CONTRATO.CD_PESSOA INNER JOIN FI_PESSOA AS EMPRESA ON EMPRESA.CD_PESSOA = CONTRATO.CD_PESSOA_PATR INNER JOIN FI_ENDERECO_PESSOA AS ENDERECO ON ENDERECO.CD_PESSOA = CONTRATO.CD_PESSOA WHERE FI_PESSOA.CD_PESSOA = @CD_PESSOA", new { CD_PESSOA });
			else if(AppSettings.IS_ORACLE_PROVIDER)
				return Conexao.QuerySingleOrDefault<DadosPessoaisEntidade>("SELECT CONTRATO.SQ_CONTRATO_TRABALHO, CONTRATO.NR_REGISTRO, CONTRATO.DT_ADMISSAO, CONTRATO.DT_DEMISSAO, FI_PESSOA.NO_PESSOA, FI_PESSOA_FISICA.IR_SEXO, FI_PESSOA_FISICA.DT_NASCIMENTO, FI_PESSOA_FISICA.NR_CPF, FI_PESSOA.CD_PESSOA, EMPRESA.NO_SIGLA AS SIGLA_EMPRESA, CASE  WHEN FI_PESSOA_FISICA.IR_SEXO='M' THEN 'MASCULINO' WHEN FI_PESSOA_FISICA.IR_SEXO='F' THEN 'FEMININO' END  AS DS_SEXO, ENDERECO.NR_CEP, ENDERECO.DS_ENDERECO, ENDERECO.NR_ENDERECO, ENDERECO.DS_COMPLEMENTO, ENDERECO.NO_BAIRRO, ENDERECO.NR_FONE, ENDERECO.NR_CELULAR, ENDERECO.NO_EMAIL FROM FI_CONTRATO_TRABALHO CONTRATO INNER  JOIN FI_PESSOA  ON FI_PESSOA.CD_PESSOA=CONTRATO.CD_PESSOA INNER  JOIN FI_PESSOA_FISICA  ON FI_PESSOA_FISICA.CD_PESSOA=CONTRATO.CD_PESSOA INNER  JOIN FI_PESSOA EMPRESA  ON EMPRESA.CD_PESSOA=CONTRATO.CD_PESSOA_PATR INNER  JOIN FI_ENDERECO_PESSOA ENDERECO  ON ENDERECO.CD_PESSOA=CONTRATO.CD_PESSOA WHERE FI_PESSOA.CD_PESSOA=:CD_PESSOA", new { CD_PESSOA });
			else
				throw new Exception("Provider não suportado!");
		}
    }
}
