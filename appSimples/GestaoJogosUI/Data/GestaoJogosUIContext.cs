using Microsoft.EntityFrameworkCore;

namespace GestaoJogosUI.Models
{
    public class GestaoJogosUIContext : DbContext
    {
        public GestaoJogosUIContext (DbContextOptions<GestaoJogosUIContext> options)
            : base(options)
        {
        }

        public DbSet<GestaoJogosUI.Models.Amigo> Amigo { get; set; }

        public DbSet<GestaoJogosUI.Models.Jogo> Jogo { get; set; }

        public DbSet<GestaoJogosUI.Models.Usuario> Usuario { get; set; }
    }
}
