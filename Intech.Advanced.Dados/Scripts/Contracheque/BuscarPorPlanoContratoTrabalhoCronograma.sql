﻿/*Config
    RetornaLista
    Retorno
        -ContrachequeEntidade
    Parametros
        -SQ_CONTRATO_TRABALHO:int
        -SQ_PLANO_PREVIDENCIAL:int
        -SQ_CRONOGRAMA:int
*/

SELECT DISTINCT 
	CRONO.DT_REFERENCIA,
	RUBRICA.DS_RUBRICA,
    FF.*
FROM FI_FICHA_FINANC_ASSISTIDO FF
INNER JOIN FI_PROCESSO_BENEFICIO PB ON PB.SQ_PROCESSO = FF.SQ_PROCESSO
INNER JOIN FI_CRONOGRAMA_CREDITO CRONO ON CRONO.SQ_CRONOGRAMA = FF.SQ_CRONOGRAMA
INNER JOIN FI_RUBRICA_FOLHA_PAGAMENTO RUBRICA ON RUBRICA.SQ_RUBRICA = FF.SQ_RUBRICA
WHERE PB.SQ_CONTRATO_TRABALHO = @SQ_CONTRATO_TRABALHO
  AND PB.SQ_PLANO_PREVIDENCIAL = @SQ_PLANO_PREVIDENCIAL
  AND CRONO.SQ_CRONOGRAMA = @SQ_CRONOGRAMA
  AND FF.IR_LANCAMENTO <> 'L'