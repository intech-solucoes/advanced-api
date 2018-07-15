/*Config
    Retorno
        -IndiceEntidade
    Parametros
        -CD_INDICE:string
*/

SELECT TOP 1 * 
FROM FI_INDICE_VALORES
WHERE CD_INDICE = @CD_INDICE
ORDER BY DT_INIC_VALIDADE DESC