#region Usings
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Intech.Advanced.BaseApi;
using Intech.Advanced.Negocio.Proxy;
using Intech.Lib.Util.Date;
using Intech.Lib.Util.Email;
using Intech.Lib.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace Intech.Advanced.FacebApi.Controllers
{
    [Route(RotasApi.SimuladorCD)]
    [ApiController]
    public class SimuladorCDController : BaseController
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            try
            {
                //TODO: Jogar pra proxy

                var sqPlano = 3;

                var contribuicoes = new ContribuicaoProxy().BuscarPorPlanoContratoTrabalho(SqContratoTrabalho, sqPlano);

                // Data de referência do salário de participação
                var dataReferencia = contribuicoes.First().DT_REFERENCIA;

                // Busca o salário de participação
                var salarioContribuicao = new SalarioContribuicaoProxy().BuscarUltimoPorContratoTrabalhoPlano(SqContratoTrabalho, sqPlano);
                var salarioParticipacao = salarioContribuicao.VL_BASE_FUNDACAO;

                // Busca o percentual de contribuição
                var percentualContribuicao = new HistManutContribuicaoProxy().BuscarUltimoPorContratoTrabalho(SqContratoTrabalho);
                var percentual = percentualContribuicao.VL_COEF_TAXA;

                return Json(new
                {
                    dataReferencia,
                    salarioParticipacao,
                    percentual
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("passo2")]
        [Authorize("Bearer")]
        public IActionResult GetPasso2()
        {
            try
            {
                //TODO: Jogar pra proxy

                var sqPlano = 3;
                var idadeMinimaAposentadoria = 48;
                var idadeMaximaAposentadoria = 70;

                var saldo = new SaldoProxy().BuscarSaldoCD(DateTime.Now, SqContratoTrabalho, sqPlano, CdPessoa).Total;
                var taxaJuros = new FatorValidadeProxy().BuscarUltimo().VL_TX_JUROS;

                var dataNascimento = new DadosPessoaisProxy().BuscarPorCdPessoa(CdPessoa).DT_NASCIMENTO.Value;
                var idadeParticipante = new Intervalo(DateTime.Now, dataNascimento).Anos;

                if (idadeParticipante > 48)
                    idadeMinimaAposentadoria = idadeParticipante;

                return Json(new
                {
                    saldo,
                    taxaJuros,
                    idadeMinimaAposentadoria,
                    idadeMaximaAposentadoria
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("simular")]
        [Authorize("Bearer")]
        public IActionResult GetSimulacao([FromBody] dynamic dados)
        {
            try
            {
                //TODO: Jogar pra proxy

                var sqPlano = 3;

                var dadosPessoais = new DadosPessoaisProxy().BuscarPorCdPessoa(CdPessoa);
                int idadeAposentadoria = Convert.ToInt32(dados.idadeAposentadoria);
                decimal contribBasica = Convert.ToDecimal(dados.contribBasica, new CultureInfo("pt-BR"));
                decimal contribFacultativa = Convert.ToDecimal(dados.contribFacultativa, new CultureInfo("pt-BR"));

                decimal saldo = new SaldoProxy().BuscarSaldoCD(DateTime.Now, SqContratoTrabalho, sqPlano, CdPessoa).Total;
                var taxaJuros = new FatorValidadeProxy().BuscarUltimo().VL_TX_JUROS;

                var dataAtual = DateTime.Now.PrimeiroDiaDoMes();
                var dataNascimento = new DadosPessoaisProxy().BuscarPorCdPessoa(CdPessoa).DT_NASCIMENTO.Value;

                var dataAposentadoria = dataNascimento.AddYears(idadeAposentadoria);
                var data = DateTime.Compare(dataAtual, dataAposentadoria) > 0 ? dataAposentadoria : dataAtual;
                var contribBruta = contribBasica * 2 + contribFacultativa;
                var taxaMensal = BuscarTaxaMensal(taxaJuros);

                var valorFuturo = saldo;

                while (data <= dataAposentadoria)
                {
                    var contribMensal = contribBruta;

                    if (data.Month == 12)
                        contribMensal *= 2;

                    valorFuturo = (valorFuturo + (valorFuturo * taxaMensal / 100)) + contribMensal;

                    data = data.AddMonths(1);
                }

                // Valor do saque
                var valorSaque = valorFuturo / 100 * (decimal)dados.saque;

                // Dependentes
                var idadeDependente = 0;
                var sexoDependente = "";
                var idadeDependenteTemporario = 0;
                var dependenteProxy = new DependenteProxy();

                // Dependente vitalício
                var dependenteVitalicio = dependenteProxy.BuscarDependentePorContratoTrabalhoDtValidadeTipo(SqContratoTrabalho, "V", dataAposentadoria);

                if (dependenteVitalicio != null)
                {
                    var dadosDependente = new DadosPessoaisProxy().BuscarDependentePorCdPessoa(dependenteVitalicio.CD_PESSOA_DEP);

                    if (dadosDependente == null)
                        throw new Exception("Não existem dados cadastrados para um de seus dependentes. Por favor, entre em contato com a Faceb.");

                    if (string.IsNullOrEmpty(dadosDependente.IR_SEXO))
                        throw new Exception("Existe um dependente cadastrado que não possui o campo \"Sexo\" preenchido. Por favor, entre em contato com a Faceb.");

                    sexoDependente = dadosDependente.IR_SEXO;

                    var dataNascDependente = dependenteVitalicio.DT_NASCIMENTO;
                    idadeDependente = new Intervalo(dataAposentadoria, dataNascDependente).Anos;
                }

                // Dependente temporário
                var dependenteTemporario = dependenteProxy.BuscarDependentePorContratoTrabalhoDtValidadeTipo(SqContratoTrabalho, "T", dataAposentadoria);

                if (dependenteTemporario != null)
                {
                    var dadosDependente = new DadosPessoaisProxy().BuscarDependentePorCdPessoa(dependenteTemporario.CD_PESSOA_DEP);

                    if (dadosDependente == null)
                        throw new Exception("Não existem dados cadastrados para um de seus dependentes. Por favor, entre em contato com a Faceb.");

                    if (string.IsNullOrEmpty(dadosDependente.IR_SEXO))
                        throw new Exception("Existe um dependente cadastrado que não possui o campo \"Sexo\" preenchido. Por favor, entre em contato com a Faceb.");

                    sexoDependente = dadosDependente.IR_SEXO;

                    var dataNascDependenteTemporario = dependenteTemporario.DT_NASCIMENTO;
                    idadeDependenteTemporario = new Intervalo(dataAposentadoria, dataNascDependenteTemporario).Anos;
                }
                
                // Fator atuarial
                var fatorAtuarialProxy = new FatorAtuarialMortalidadeProxy();
                var axiPar = fatorAtuarialProxy.BuscarPorIdadeSexo(idadeAposentadoria, dadosPessoais.IR_SEXO).VL_FATOR_A.Value;

                var axiDep = 0M;
                var axyi = 0M;
                if (idadeDependente > 0)
                {
                    axiDep = fatorAtuarialProxy.BuscarPorIdadeSexo(idadeDependente, sexoDependente).VL_FATOR_A.Value;
                    var fator = fatorAtuarialProxy.BuscarPorIdadePartIdadeDepSexo(idadeAposentadoria, idadeDependente, sexoDependente);
                    axyi = fator.VL_FATOR_A.Value;
                }

                var prazoDepentendeTemporario = 20 - idadeDependenteTemporario;
                var xn = idadeAposentadoria + prazoDepentendeTemporario;

                var fatorDxn = fatorAtuarialProxy.BuscarPorTabelaIdadeSexo("lxdx", xn, dadosPessoais.IR_SEXO).VL_FATOR_B.Value;
                var fatorDx = fatorAtuarialProxy.BuscarPorTabelaIdadeSexo("lxdx", idadeAposentadoria, dadosPessoais.IR_SEXO).VL_FATOR_B.Value;

                var fatorAxn = fatorAtuarialProxy.BuscarPorTabelaIdadeSexo("ax", xn, dadosPessoais.IR_SEXO).VL_FATOR_A.Value;

                decimal fatorAn = 0;
                if (prazoDepentendeTemporario > 0 && !string.IsNullOrEmpty(sexoDependente))
                    fatorAn = fatorAtuarialProxy.BuscarPorTabelaIdadeSexo("an", prazoDepentendeTemporario, sexoDependente).VL_FATOR_A.Value;

                var apuracaoAxn = axiPar - (fatorDxn / fatorDx) * fatorAxn;

                var fatorAtuarialSemPensaoMorte = 13 * axiPar;
                var fatorAtuarialPensaoMorte = 13 * (axiPar + Math.Max(fatorAn - apuracaoAxn, axiDep - axyi));
                
                // Renda por prazos indeterminados
                var rendaPrazoIndeterminadoPensaoMorte = (valorFuturo - valorSaque) / fatorAtuarialPensaoMorte;
                var rendaPrazoIndeterminadoSemPensaoMorte = (valorFuturo - valorSaque) / fatorAtuarialSemPensaoMorte;

                // Renda por prazo certo
                var listaPrazos = new List<KeyValuePair<int, decimal>>();

                for (int prazo = 15; prazo <= 25; prazo++)
                {
                    decimal valor = (valorFuturo - valorSaque) / (prazo * 13);
                    listaPrazos.Add(new KeyValuePair<int, decimal>(prazo, valor));
                }

                // Renda por percentual do saldo de contas
                var listaSaldoPercentuais = new List<KeyValuePair<string, decimal>>();

                for (decimal percentual = 0.5M; percentual <= 2.0M; percentual += 0.5M)
                {
                    decimal valor = (valorFuturo - valorSaque) * percentual / 100;
                    listaSaldoPercentuais.Add(new KeyValuePair<string, decimal>(percentual.ToString("N1").Replace(".", ","), valor));
                }

                return Json(new
                {
                    valorFuturo,
                    dataAposentadoria,
                    valorSaque,
                    idadeDependente,
                    fatorAtuarialPensaoMorte,
                    fatorAtuarialSemPensaoMorte,
                    rendaPrazoIndeterminadoPensaoMorte,
                    rendaPrazoIndeterminadoSemPensaoMorte,
                    listaPrazos,
                    listaSaldoPercentuais
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        decimal BuscarTaxaMensal(decimal taxaAnual)
        {
            var mensal = 1M / 12M;
            var valor = taxaAnual / 100 + 1;
            return (decimal)(Math.Pow((double)valor, (double)mensal) - 1) * 100;
        }

        [HttpPost("simularNaoParticipante")]
        [AllowAnonymous]
        public IActionResult GetSimulacaoNaoParticipante([FromBody] DadosSimulacaoNaoParticipante dados)
        {
            try
            {
                var dataAtual = DateTime.Now.PrimeiroDiaDoMes();

                var dataAposentadoria = dados.DataNascimento.AddYears(dados.IdadeAposentadoria);
                if (dataAposentadoria < DateTime.Now)
                    return BadRequest("Data inválida. Insira uma data de nascimento inferior ou uma idade de aposentadoria superior.");

                var data = DateTime.Compare(dataAtual, dataAposentadoria) > 0 ? dataAposentadoria : dataAtual;

                var contribFacultativa = dados.ContribFacultativa.HasValue ? dados.ContribFacultativa.Value : 0;

                var contribBruta = dados.ContribBasica * 2 + contribFacultativa;
                var taxaMensal = BuscarTaxaMensal(dados.TaxaJuros);

                var valorFuturo = dados.AporteInicial;

                while (data <= dataAposentadoria)
                {
                    var contribMensal = contribBruta;

                    if (data.Month == 12)
                        contribMensal *= 2;

                    valorFuturo = (valorFuturo + (valorFuturo * taxaMensal / 100)) + contribMensal;

                    data = data.AddMonths(1);
                }

                // Valor do saque
                decimal valorSaque = valorFuturo / 100 * dados.Saque;

                // Dependentes
                var idadeDependente = 0;
                var idadeDependenteTemporario = 0;
                var dependenteProxy = new DependenteProxy();

                // Dependente vitalício
                DateTime? dataNascConjugue = dados.NascimentoConjugue;
                DateTime? dataNascFilhoInvalido = dados.NascimentoFilhoInvalido;
                DateTime? dataNascDependente;

                if (dataNascConjugue != null && dataNascFilhoInvalido != null)
                    dataNascDependente = dataNascConjugue > dataNascFilhoInvalido ? dataNascConjugue : dataNascFilhoInvalido;
                else if (dataNascConjugue != null && dataNascFilhoInvalido == null)
                    dataNascDependente = dataNascConjugue;
                else
                    dataNascDependente = dataNascFilhoInvalido == null ? null : dataNascFilhoInvalido;

                /// var dependenteVitalicio = dependenteProxy.BuscarDependentePorContratoTrabalhoDtValidadeTipo(SqContratoTrabalho, "V", dataAposentadoria);

                if (dataNascDependente != null)
                {
                    // dataNascDependente = Convert.ToDateTime(dataNascDependente);
                    idadeDependente = new Intervalo(dataAposentadoria, dataNascDependente.Value).Anos;
                }

                // Dependente temporário
                if (dados.NascimentoFilhoMaisNovo.HasValue)
                {
                    idadeDependenteTemporario = new Intervalo(dataAposentadoria, dados.NascimentoFilhoMaisNovo.Value).Anos;
                }

                // Fator atuarial
                var fatorAtuarialProxy = new FatorAtuarialMortalidadeProxy();
                var axiPar = fatorAtuarialProxy.BuscarPorIdadeSexo(dados.IdadeAposentadoria, dados.Sexo).VL_FATOR_A.Value;

                var axiDep = 0M;
                var axyi = 0M;
                if (idadeDependente > 0)
                {
                    axiDep = fatorAtuarialProxy.BuscarPorIdadeSexo(idadeDependente, dados.SexoFilhoMaisNovo).VL_FATOR_A.Value;
                    var fator = fatorAtuarialProxy.BuscarPorIdadePartIdadeDepSexo(dados.IdadeAposentadoria, idadeDependente, dados.SexoFilhoMaisNovo);
                    axyi = fator.VL_FATOR_A.Value;
                }

                var prazoDepentendeTemporario = 20 - idadeDependenteTemporario;
                if (prazoDepentendeTemporario < 0)
                    prazoDepentendeTemporario = 0;
                var xn = dados.IdadeAposentadoria + prazoDepentendeTemporario;

                var fatorDxn = fatorAtuarialProxy.BuscarPorTabelaIdadeSexo("lxdx", xn, dados.Sexo).VL_FATOR_B.Value;
                var fatorDx = fatorAtuarialProxy.BuscarPorTabelaIdadeSexo("lxdx", dados.IdadeAposentadoria, dados.Sexo).VL_FATOR_B.Value;

                var fatorAxn = fatorAtuarialProxy.BuscarPorTabelaIdadeSexo("ax", xn, dados.Sexo).VL_FATOR_A.Value;

                decimal fatorAn = 0;
                if(prazoDepentendeTemporario > 0 && dados.SexoFilhoInvalido != null)
                    fatorAn = fatorAtuarialProxy.BuscarPorTabelaIdadeSexo("an", prazoDepentendeTemporario, dados.SexoFilhoInvalido).VL_FATOR_A.Value;

                var apuracaoAxn = axiPar - (fatorDxn / fatorDx) * fatorAxn;

                var fatorAtuarialSemPensaoMorte = 13 * axiPar;
                var fatorAtuarialPensaoMorte = 13 * (axiPar + Math.Max(fatorAn - apuracaoAxn, axiDep - axyi));

                // Renda por prazos indeterminados
                var rendaPrazoIndeterminadoPensaoMorte = (valorFuturo - valorSaque) / fatorAtuarialPensaoMorte;
                var rendaPrazoIndeterminadoSemPensaoMorte = (valorFuturo - valorSaque) / fatorAtuarialSemPensaoMorte;

                // Renda por prazo certo
                var listaPrazos = new List<KeyValuePair<int, decimal>>();

                for (int prazo = 15; prazo <= 25; prazo++)
                {
                    decimal valor = (valorFuturo - valorSaque) / (prazo * 13);
                    listaPrazos.Add(new KeyValuePair<int, decimal>(prazo, valor));
                }

                // Renda por percentual do saldo de contas
                var listaSaldoPercentuais = new List<KeyValuePair<string, decimal>>();

                for (decimal percentual = 0.5M; percentual <= 2.0M; percentual += 0.5M)
                {
                    decimal valor = (valorFuturo - valorSaque) * percentual / 100;
                    listaSaldoPercentuais.Add(new KeyValuePair<string, decimal>(percentual.ToString("N1").Replace(".", ","), valor));
                }

                return Json(new
                {
                    nome = dados.Nome,
                    email = dados.Email,
                    valorFuturo,
                    dataAposentadoria,
                    valorSaque,
                    idadeDependente,
                    fatorAtuarialPensaoMorte,
                    fatorAtuarialSemPensaoMorte,
                    rendaPrazoIndeterminadoPensaoMorte,
                    rendaPrazoIndeterminadoSemPensaoMorte,
                    listaPrazos,
                    listaSaldoPercentuais
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("aderir")]
        [AllowAnonymous]
        public IActionResult EnviarAdesao([FromBody] dynamic dados)
        {
            try
            {
                var emailConfig = AppSettings.Get().Email;
                var corpoEmail =
                    $"Uma Simulação de Benefício no mobile foi realizada pelo usuário:<br/>" +
                    $"Nome: {dados.nome}<br/>" +
                    $"E-mail: {dados.email}<br/>";
                EnvioEmail.EnviarMailKit(emailConfig, emailConfig.EmailRelacionamento, $"Faceb - Simulação de Não Participante", corpoEmail);

                return Json("Um e-mail com os dados da sua simulação foi enviado para a Faceb! ");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

    public class DadosSimulacaoNaoParticipante
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public int IdadeAposentadoria { get; set; }
        public decimal ContribBasica { get; set; }
        public decimal? ContribFacultativa { get; set; }
        public decimal TaxaJuros { get; set; }
        public decimal AporteInicial { get; set; }
        public decimal Saque { get; set; }

        public DateTime? NascimentoConjugue { get; set; }

        public DateTime? NascimentoFilhoInvalido { get; set; }
        public string SexoFilhoInvalido { get; set; }

        public DateTime? NascimentoFilhoMaisNovo { get; set; }
        public string SexoFilhoMaisNovo { get; set; }
    }
}