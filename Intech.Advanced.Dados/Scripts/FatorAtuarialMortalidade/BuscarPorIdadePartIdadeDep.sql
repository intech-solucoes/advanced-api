﻿/*Config
    Retorno
        -FatorAtuarialMortalidadeEntidade
    Parametros
        -IDADE_PART:int
        -IDADE_DEP:int
*/

SELECT *
FROM  FI_FATOR_ATUARIAL_MORTALIDADE
WHERE (FI_FATOR_ATUARIAL_MORTALIDADE.IC_TABELA = 'AXY') 
  AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_TIT = @IDADE_PART) 
  AND (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_DEP = @IDADE_DEP)