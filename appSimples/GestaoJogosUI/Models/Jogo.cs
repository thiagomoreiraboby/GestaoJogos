﻿using System.ComponentModel.DataAnnotations;

namespace GestaoJogosUI.Models
{
    public class Jogo: EntidadeBase
    {
        public Jogo()
        {
        }
        [Required(ErrorMessage = "Selecione um amigo.")]
        [Display(Name = "Amigo")]
        public int? AmigoID { get; set; }
        public Amigo Amigo { get; set; }
    }
}
