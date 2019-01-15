/*Config
    Retorno
        -UsuarioGrupoEntidade
    Parametros
        -USR_CODIGO:int
*/

SELECT * FROM FR_USUARIO_GRUPO
WHERE USR_CODIGO = @USR_CODIGO
