﻿/*Config
    Retorno
        -SaldoEntidade
    Parametros
        -DT_REFERENCIA:DateTime
        -SQ_PLANO_PREVIDENCIAL:int
        -SQ_CONTRATO_TRABALHO:int
*/

SELECT SUM(FI_FICHA_CONTRIB_PREVIDENCIAL.QT_COTA_CONTRIBUICAO) AS QT_COTA_CONTRIBUICAO
FROM FI_FICHA_CONTRIB_PREVIDENCIAL
INNER JOIN  FI_TIPO_COBRANCA ON FI_TIPO_COBRANCA.SQ_TIPO_COBRANCA = FI_FICHA_CONTRIB_PREVIDENCIAL.SQ_TIPO_COBRANCA 
WHERE (FI_FICHA_CONTRIB_PREVIDENCIAL.DT_REFERENCIA BETWEEN @DT_REFERENCIA AND GETDATE()) 
  AND (FI_FICHA_CONTRIB_PREVIDENCIAL.SQ_TIPO_FUNDO = 3) 
  AND (FI_FICHA_CONTRIB_PREVIDENCIAL.SQ_CONTRATO_TRABALHO = @SQ_CONTRATO_TRABALHO) 
  AND (FI_FICHA_CONTRIB_PREVIDENCIAL.SQ_PLANO_PREVIDENCIAL = @SQ_PLANO_PREVIDENCIAL)
  AND (FI_TIPO_COBRANCA.EE_AUTOPATROCINIO = 'S')