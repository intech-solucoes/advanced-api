/*Config
    RetornaLista
    Retorno
        -IndiceEntidade
    Parametros
        -CD_INDICE:string
*/

SELECT * 
FROM FI_INDICE_VALORES
WHERE CD_INDICE = @CD_INDICE
  AND EE_REAL = 'S'
ORDER BY DT_INIC_VALIDADE DESC