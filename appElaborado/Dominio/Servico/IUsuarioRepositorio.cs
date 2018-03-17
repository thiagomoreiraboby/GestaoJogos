using System.Threading.Tasks;
using Dominio.Model;

namespace Dominio.Servico
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {
        Task<Usuario> LogarAsync(Usuario usuario);
    }
}
