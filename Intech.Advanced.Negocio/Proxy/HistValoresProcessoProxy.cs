using Intech.Advanced.Dados.DAO;
using System.Data;

namespace Intech.Advanced.Negocio.Proxy
{
	public class HistValoresProcessoProxy : HistValoresProcessoDAO
	{
		public HistValoresProcessoProxy (IDbTransaction tx = null) : base(tx) { }
	}
}
