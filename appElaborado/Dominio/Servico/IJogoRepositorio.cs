using Dominio.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Servico
{
    public interface IJogoRepositorio: IRepositorioBase<Jogo>
    {
        Task<IEnumerable<Jogo>> JogosEmprestadosAsync();
        Task<IEnumerable<Jogo>> JogosComigoAsync();
        Task<IEnumerable<Jogo>> PesquisarTodoscomIncludAsync();

    }
}
