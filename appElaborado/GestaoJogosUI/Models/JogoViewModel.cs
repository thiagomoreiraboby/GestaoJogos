using System.ComponentModel.DataAnnotations;

namespace GestaoJogosUI.Models
{
    public class JogoViewModel : EntidadeBaseViewModel
    {
        [Required(ErrorMessage = "Selecione um amigo.")]
        [Display(Name = "Amigo")]
        public int? AmigoID { get; set; }
        public AmigoViewModel Amigo { get; set; }
    }
}
