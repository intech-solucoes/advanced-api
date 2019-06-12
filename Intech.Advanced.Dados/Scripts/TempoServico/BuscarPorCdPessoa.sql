/*Config
    RetornaLista
    Retorno
        -TempoServicoEntidade
    Parametros
        -CD_PESSOA_EMPREGADO:int
*/

SELECT * FROM FI_TEMPO_SERVICO 
WHERE CD_PESSOA_EMPREGADO = @CD_PESSOA_EMPREGADO
ORDER BY DT_INIC_ATIVIDADE