using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoJogosUI.Models
{
    public class EntidadeBaseViewModel
    {
        [Key]
        [Display(Name = "Código")]
        public int? ID { get; set; }
        [Required(ErrorMessage = "Digite o nome.")]
        [MinLength(3, ErrorMessage = "O tamanho mínimo do nome são 3 caracteres.")]
        [StringLength(200, ErrorMessage = "O tamanho máximo são 200 caracteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        public object StyleOfWritting { get; internal set; }
    }
}
