/*Config
    Retorno
        -UsuarioEntidade
    Parametros
        -USR_LOGIN:string
        -USR_SENHA:string
*/

SELECT *
FROM FR_USUARIO
WHERE USR_LOGIN = @USR_LOGIN
  AND USR_SENHA = @USR_SENHA