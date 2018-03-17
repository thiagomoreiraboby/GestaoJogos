using Dominio.Model;
using Dominio.Servico;
using Microsoft.EntityFrameworkCore;
using Repositorio.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio.Repositorio
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(GestaoJogosUIContext context) : base(context)
        {
        }

        public async Task<Usuario> LogarAsync(Usuario usuario)
        {
            return await _context.Usuario.SingleOrDefaultAsync(x => x.Nome == usuario.Nome && x.Senha == usuario.Senha);
        }
    }
}
