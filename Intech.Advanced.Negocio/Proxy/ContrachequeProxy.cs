using System;
using System.Collections.Generic;
using System.Linq;
using Intech.Advanced.Dados.DAO;
using Intech.Advanced.Entidades;

namespace Intech.Advanced.Negocio.Proxy
{
    public class ContrachequeProxy : ContrachequeDAO
    {
        public dynamic BuscarRubricasPorPlanoContratoTrabalhoCronograma(int SQ_CONTRATO_TRABALHO, int SQ_PLANO_PREVIDENCIAL, int SQ_CRONOGRAMA, bool pensionista, int SQ_PESSOA_RECEB)
        {
            List<ContrachequeEntidade> rubricas;

            if(pensionista)
                rubricas = base.BuscarPorPlanoContratoTrabalhoCronogramaRecebedor(SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL, SQ_CRONOGRAMA, SQ_PESSOA_RECEB).ToList();
            else
                rubricas = base.BuscarPorPlanoContratoTrabalhoCronograma(SQ_CONTRATO_TRABALHO, SQ_PLANO_PREVIDENCIAL, SQ_CRONOGRAMA).ToList();
            
            var bruto = rubricas.Where(x => x.IR_LANCAMENTO == "P").Sum(x => x.VL_CALCULO).Value;
            var descontos = rubricas.Where(x => x.IR_LANCAMENTO == "D").Sum(x => x.VL_CALCULO).Value;
            var liquido = bruto - Math.Abs(descontos);

            return new {
                rubricas,
                resumo = new {
                    bruto,
                    descontos,
                    liquido,
                    referencia = rubricas.First().DT_REFERENCIA.Value.ToString("MM/yyyy")
                }
            };
        }
    }
}
