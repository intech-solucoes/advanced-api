/*Config
    Retorno
        -UsuarioEntidade
    Parametros
        -CPF:string
*/

SELECT * FROM fr_usuario
WHERE USR_LOGIN = @CPF