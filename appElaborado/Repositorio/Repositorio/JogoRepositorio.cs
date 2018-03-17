using Dominio.Model;
using Dominio.Servico;
using Microsoft.EntityFrameworkCore;
using Repositorio.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio.Repositorio
{
    public class JogoRepositorio : RepositorioBase<Jogo>, IJogoRepositorio
    {
        public JogoRepositorio(GestaoJogosUIContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Jogo>> JogosComigoAsync()
        {
            return await _context.Jogo.Include(j => j.Amigo).Where(x => x.AmigoID == 1).ToArrayAsync();
        }

        public async Task<IEnumerable<Jogo>> JogosEmprestadosAsync()
        {
            return await _context.Jogo.Include(j => j.Amigo).Where(x => x.AmigoID > 1).ToListAsync();
        }

        public async Task<IEnumerable<Jogo>> PesquisarTodoscomIncludAsync()
        {
            return await _context.Jogo.Include(j => j.Amigo).ToListAsync();
        }
    }
}
