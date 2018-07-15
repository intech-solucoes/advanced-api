/*Config
    RetornaLista
    Retorno
        -IndiceEntidade
    Parametros
        -CD_INDICE:string
        -DT_INI:DateTime
        -DT_FIM:DateTime
*/

SELECT * 
FROM FI_INDICE_VALORES
WHERE CD_INDICE = @CD_INDICE
  and DT_INIC_VALIDADE >= @DT_INI
  and DT_INIC_VALIDADE <= @DT_FIM
ORDER BY DT_INIC_VALIDADE DESC