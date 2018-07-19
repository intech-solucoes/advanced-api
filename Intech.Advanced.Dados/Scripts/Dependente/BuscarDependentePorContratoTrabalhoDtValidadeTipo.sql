﻿/*Config
    Retorno
        -DependenteEntidade
    Parametros
        -SQ_CONTRATO_TRABALHO:int
        -TIPO:string
        -DT_VALIDADE:DateTime
*/

SELECT TOP 1 C.*
FROM FI_DEPENDENTE A
  INNER JOIN  FI_CONTRATO_TRABALHO B ON B.CD_PESSOA = A.CD_PESSOA_PAI
  INNER JOIN  FI_HIST_DEPENDENTE_PREVIDENCIAL X ON (X.CD_PESSOA = A.CD_PESSOA_PAI)
    AND (X.CD_PESSOA_DEP = A.CD_PESSOA_DEP)
  INNER JOIN FI_PESSOA_FISICA C ON C.CD_PESSOA = A.CD_PESSOA_DEP
  INNER JOIN FI_GRAU_PARENTESCO D ON (D.CD_GRAU_PARENTESCO = A.CD_GRAU_PARENTESCO) 
    AND (D.IR_TIPO_VALIDADE = @TIPO)
WHERE (B.SQ_CONTRATO_TRABALHO = @SQ_CONTRATO_TRABALHO) 
  AND (X.SQ_PLANO_PREVIDENCIAL = 3) 
  AND (A.DT_TERM_VALIDADE >= @DT_VALIDADE)
ORDER BY C.DT_NASCIMENTO DESC 