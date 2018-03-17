using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestaoJogosUI.Models;

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
    }
}
