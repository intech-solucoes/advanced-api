/*Config
    Retorno
        -FatorAtuarialMortalidadeEntidade
    Parametros
        -IC_TABELA:string
        -IDADE:int
*/

SELECT  *
FROM  FI_FATOR_ATUARIAL_MORTALIDADE
WHERE  (FI_FATOR_ATUARIAL_MORTALIDADE.IC_TABELA = @IC_TABELA) AND
  (FI_FATOR_ATUARIAL_MORTALIDADE.NR_IDADE_TIT = @IDADE)