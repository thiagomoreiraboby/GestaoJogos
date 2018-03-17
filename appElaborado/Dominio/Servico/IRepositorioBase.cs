using Dominio.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Servico
{
    public interface IRepositorioBase<Tentity> where Tentity : EntidadeBase
    {
        Task SalvarAsync(Tentity entidade);
        Task DeleteAsync(Tentity entidade);
        Task<IEnumerable<Tentity>> PesquisarTodosAsync();
        Task<Tentity> PesquisarporIdAsync(int id);
    }
}
