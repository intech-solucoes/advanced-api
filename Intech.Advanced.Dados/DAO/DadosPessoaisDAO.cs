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
        
		public virtual DadosPessoaisEntidade BuscarPensionistaPorCdPessoa(int CD_PESSOA)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<DadosPessoaisEntidade>("SELECT fi_pessoa.no_pessoa,         fi_pessoa_fisica.ir_sexo,         fi_pessoa_fisica.dt_nascimento,         fi_pessoa_fisica.nr_cpf,         fi_pessoa.cd_pessoa,         CASE           WHEN fi_pessoa_fisica.ir_sexo = 'M' THEN 'MASCULINO'           WHEN fi_pessoa_fisica.ir_sexo = 'F' THEN 'FEMININO'         END AS DS_SEXO,         ENDERECO.nr_cep,         ENDERECO.ds_endereco,         ENDERECO.nr_endereco,         ENDERECO.ds_complemento,         ENDERECO.no_bairro,         ENDERECO.nr_fone,         ENDERECO.nr_celular,         ENDERECO.no_email  FROM   fr_usuario a         INNER JOIN fr_usuario_grupo b                 ON b.usr_codigo = a.usr_codigo         INNER JOIN fr_grupo c                 ON c.grp_codigo = b.grp_codigo         INNER JOIN fi_pessoa                 ON fi_pessoa.cd_pessoa = a.cd_pessoa         INNER JOIN fi_pessoa_fisica                 ON fi_pessoa_fisica.cd_pessoa = a.cd_pessoa         INNER JOIN fi_endereco_pessoa AS ENDERECO                 ON ENDERECO.cd_pessoa = a.cd_pessoa  WHERE  ic_categ_usuario = 'PE'         AND fi_pessoa.cd_pessoa = @CD_PESSOA", new { CD_PESSOA });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<DadosPessoaisEntidade>("SELECT FI_PESSOA.NO_PESSOA, FI_PESSOA_FISICA.IR_SEXO, FI_PESSOA_FISICA.DT_NASCIMENTO, FI_PESSOA_FISICA.NR_CPF, FI_PESSOA.CD_PESSOA, CASE  WHEN FI_PESSOA_FISICA.IR_SEXO='M' THEN 'MASCULINO' WHEN FI_PESSOA_FISICA.IR_SEXO='F' THEN 'FEMININO' END  AS DS_SEXO, ENDERECO.NR_CEP, ENDERECO.DS_ENDERECO, ENDERECO.NR_ENDERECO, ENDERECO.DS_COMPLEMENTO, ENDERECO.NO_BAIRRO, ENDERECO.NR_FONE, ENDERECO.NR_CELULAR, ENDERECO.NO_EMAIL FROM FR_USUARIO  A  INNER  JOIN FR_USUARIO_GRUPO   B  ON B.USR_CODIGO=A.USR_CODIGO INNER  JOIN FR_GRUPO   C  ON C.GRP_CODIGO=B.GRP_CODIGO INNER  JOIN FI_PESSOA  ON FI_PESSOA.CD_PESSOA=A.CD_PESSOA INNER  JOIN FI_PESSOA_FISICA  ON FI_PESSOA_FISICA.CD_PESSOA=A.CD_PESSOA INNER  JOIN FI_ENDERECO_PESSOA ENDERECO  ON ENDERECO.CD_PESSOA=A.CD_PESSOA WHERE IC_CATEG_USUARIO='PE' AND FI_PESSOA.CD_PESSOA=:CD_PESSOA", new { CD_PESSOA });
				else
					throw new Exception("Provider não suportado!");
			}
			finally
			{
				Conexao.Close();
			}
		}

		public virtual DadosPessoaisEntidade BuscarPorCdPessoa(int CD_PESSOA)
		{
			try
			{
				if(AppSettings.IS_SQL_SERVER_PROVIDER)
					return Conexao.QuerySingleOrDefault<DadosPessoaisEntidade>("SELECT      CONTRATO.SQ_CONTRATO_TRABALHO,      CONTRATO.NR_REGISTRO,      CONTRATO.DT_ADMISSAO,      CONTRATO.DT_DEMISSAO,      FI_PESSOA.NO_PESSOA,      FI_PESSOA_FISICA.IR_SEXO,      FI_PESSOA_FISICA.DT_NASCIMENTO,      FI_PESSOA_FISICA.NR_CPF,     FI_PESSOA.CD_PESSOA,     EMPRESA.NO_SIGLA AS SIGLA_EMPRESA, 	CASE 		WHEN FI_PESSOA_FISICA.IR_SEXO = 'M' THEN 'MASCULINO' 		WHEN FI_PESSOA_FISICA.IR_SEXO = 'F' THEN 'FEMININO' 	END AS DS_SEXO, 	ENDERECO.NR_CEP, 	ENDERECO.DS_ENDERECO, 	ENDERECO.NR_ENDERECO, 	ENDERECO.DS_COMPLEMENTO, 	ENDERECO.NO_BAIRRO, 	ENDERECO.NR_FONE, 	ENDERECO.NR_CELULAR, 	ENDERECO.NO_EMAIL FROM FI_CONTRATO_TRABALHO AS CONTRATO INNER JOIN FI_PESSOA ON FI_PESSOA.CD_PESSOA = CONTRATO.CD_PESSOA INNER JOIN FI_PESSOA_FISICA ON FI_PESSOA_FISICA.CD_PESSOA = CONTRATO.CD_PESSOA INNER JOIN FI_PESSOA AS EMPRESA ON EMPRESA.CD_PESSOA = CONTRATO.CD_PESSOA_PATR INNER JOIN FI_ENDERECO_PESSOA AS ENDERECO ON ENDERECO.CD_PESSOA = CONTRATO.CD_PESSOA WHERE FI_PESSOA.CD_PESSOA = @CD_PESSOA", new { CD_PESSOA });
				else if(AppSettings.IS_ORACLE_PROVIDER)
					return Conexao.QuerySingleOrDefault<DadosPessoaisEntidade>("SELECT CONTRATO.SQ_CONTRATO_TRABALHO, CONTRATO.NR_REGISTRO, CONTRATO.DT_ADMISSAO, CONTRATO.DT_DEMISSAO, FI_PESSOA.NO_PESSOA, FI_PESSOA_FISICA.IR_SEXO, FI_PESSOA_FISICA.DT_NASCIMENTO, FI_PESSOA_FISICA.NR_CPF, FI_PESSOA.CD_PESSOA, EMPRESA.NO_SIGLA AS SIGLA_EMPRESA, CASE  WHEN FI_PESSOA_FISICA.IR_SEXO='M' THEN 'MASCULINO' WHEN FI_PESSOA_FISICA.IR_SEXO='F' THEN 'FEMININO' END  AS DS_SEXO, ENDERECO.NR_CEP, ENDERECO.DS_ENDERECO, ENDERECO.NR_ENDERECO, ENDERECO.DS_COMPLEMENTO, ENDERECO.NO_BAIRRO, ENDERECO.NR_FONE, ENDERECO.NR_CELULAR, ENDERECO.NO_EMAIL FROM FI_CONTRATO_TRABALHO CONTRATO INNER  JOIN FI_PESSOA  ON FI_PESSOA.CD_PESSOA=CONTRATO.CD_PESSOA INNER  JOIN FI_PESSOA_FISICA  ON FI_PESSOA_FISICA.CD_PESSOA=CONTRATO.CD_PESSOA INNER  JOIN FI_PESSOA EMPRESA  ON EMPRESA.CD_PESSOA=CONTRATO.CD_PESSOA_PATR INNER  JOIN FI_ENDERECO_PESSOA ENDERECO  ON ENDERECO.CD_PESSOA=CONTRATO.CD_PESSOA WHERE FI_PESSOA.CD_PESSOA=:CD_PESSOA", new { CD_PESSOA });
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
