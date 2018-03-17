using Dominio.Model;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Context
{
    public class GestaoJogosUIContext : DbContext
    {
        public GestaoJogosUIContext (DbContextOptions<GestaoJogosUIContext> options)
            : base(options)
        {
        }

        public DbSet<Amigo> Amigo { get; set; }

        public DbSet<Jogo> Jogo { get; set; }

        public DbSet<Usuario> Usuario { get; set; }
    }
}
