using Dominio.Model;
using Dominio.Servico;
using Repositorio.Context;

namespace Repositorio.Repositorio
{
    public class AmigoRepositorio : RepositorioBase<Amigo>, IAmigoRepositorio
    {
        public AmigoRepositorio(GestaoJogosUIContext context) : base(context)
        {
        }
    }
}
