#region Usings
using Intech.Advanced.Dados.DAO;
using System;
using System.Linq;
#endregion

namespace Intech.Advanced.Negocio.Proxy
{
    public class SaldoProxy : SaldoDAO
    {
        public dynamic BuscarSaldoBD(DateTime DT_REFERENCIA, int SQ_CONTRATO_TRABALHO, int SQ_PLANO_PREVIDENCIAL, int CD_PESSOA)
        {
            // Busca valor bruto da reserva individual
            var lista = base.BuscarBrutoPorPlanoReferenciaPessoa(DT_REFERENCIA, SQ_PLANO_PREVIDENCIAL, CD_PESSOA).ToList();
            var valIndiceFaceb1 = new IndiceProxy().BuscarUltimoPorCdIndice("FACEB I").VL_INDICE;

            var brutoReservaIndividual = lista.Sum(x => (x.QT_COTA_CONTRIBUICAO * x.VL_RESGATE / 100) * valIndiceFaceb1);

            // Busca taxa adm
            var taxaAdm = BuscarTaxaAdm(DT_REFERENCIA, SQ_PLANO_PREVIDENCIAL, SQ_CONTRATO_TRABALHO).QT_COTA_CONTRIBUICAO * valIndiceFaceb1;

            var contribuicoesAntes2011 = BuscarAutopatrocinioPorReferenciaAnterior(new DateTime(2011, 12, 31), SQ_PLANO_PREVIDENCIAL, SQ_CONTRATO_TRABALHO).QT_COTA_CONTRIBUICAO * valIndiceFaceb1;
            var contribuicoesApos2011 = BuscarAutopatrocinioPorReferenciaApos(new DateTime(2011, 12, 31), SQ_PLANO_PREVIDENCIAL, SQ_CONTRATO_TRABALHO).QT_COTA_CONTRIBUICAO * valIndiceFaceb1;

            var reservaIndividual = brutoReservaIndividual - taxaAdm;
            var autopatrocinio = (contribuicoesApos2011 * 0.9M) + contribuicoesAntes2011;

            if (autopatrocinio == null)
                autopatrocinio = 0;

            return new
            {
                ReservaIndividual = reservaIndividual,
                SaldoContaAutopatrocinio = autopatrocinio,
                Resgate = (reservaIndividual * 1.35M) + autopatrocinio,
                Portabilidade = (reservaIndividual * 2M) + autopatrocinio
            };
        }
    }
}