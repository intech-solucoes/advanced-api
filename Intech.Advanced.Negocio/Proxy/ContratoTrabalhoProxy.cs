using Intech.Advanced.Dados.DAO;
using System.Data;

namespace Intech.Advanced.Negocio.Proxy
{
	public class ContratoTrabalhoProxy : ContratoTrabalhoDAO
	{
		public ContratoTrabalhoProxy (IDbTransaction tx = null) : base(tx) { }
	}
}