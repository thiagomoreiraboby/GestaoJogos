using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoJogosUI.Models
{
    public class Jogo
    {
        public int? ID { get; set; }
        public string Nome { get; set; }
        public int? AmigoID { get; set; }
        public Amigo Amigo { get; set; }
    }
}
