/*Config
    RetornaLista
    Retorno
        -CalendarioPgtEntidade
    Parametros
        -CD_PLANO:string
*/

SELECT *
FROM WEB_CALENDARIO_PGT
WHERE CD_PLANO = @CD_PLANO