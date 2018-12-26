#region Usings
using Intech.Advanced.Dados.DAO;
using Intech.Advanced.Entidades;
using System.Security.Cryptography;
using System.Text; 
#endregion

namespace Intech.Advanced.Negocio.Proxy
{
    public class UsuarioProxy : UsuarioDAO
    {
        public override UsuarioEntidade BuscarPorLoginSenha(string USR_LOGIN, string USR_SENHA)
        {
            // Busca usuario existente para utilizar o USR_CODIGO
            var user = base.BuscarPorCPF(USR_LOGIN);

            // Concatena o USR_CODIGO com a senha e encripta utilizando MD5
            var senha = GerarHashMd5(user.USR_CODIGO + USR_SENHA);
            //var senha = GerarHashMd5(3352 + USR_SENHA);

            return base.BuscarPorLoginSenha(USR_LOGIN, senha);
        }

        private string GerarHashMd5(string input)
        {
            var md5Hash = MD5.Create();
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
