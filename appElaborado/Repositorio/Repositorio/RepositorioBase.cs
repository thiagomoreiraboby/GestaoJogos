using Dominio.Model;
using Dominio.Servico;
using Microsoft.EntityFrameworkCore;
using Repositorio.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio.Repositorio
{
    public class RepositorioBase<TEntity> : IRepositorioBase<TEntity> where TEntity : EntidadeBase
    {
        protected readonly GestaoJogosUIContext _context;
        public RepositorioBase(GestaoJogosUIContext context)
        {
            if (_context == null)
                _context = context;
        }
        public async Task DeleteAsync(TEntity entidade)
        {
             _context.Remove(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> PesquisarporIdAsync(int id)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<TEntity>> PesquisarTodosAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task SalvarAsync(TEntity entidade)
        {
            if (entidade.ID == null)
                _context.Add(entidade);
            else _context.Update(entidade);
            await _context.SaveChangesAsync();
        }
    }
}
