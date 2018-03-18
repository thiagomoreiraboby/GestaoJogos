using System.ComponentModel.DataAnnotations;

namespace GestaoJogosUI.Models
{
    public class UsuarioViewModel : EntidadeBaseViewModel
    {
        [Required(ErrorMessage = "Digite a senha.")]
        [MinLength(1, ErrorMessage = "O tamanho mínimo do nome são 1 caracteres.")]
        [StringLength(20, ErrorMessage = "O tamanho máximo são 20 caracteres.")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
    }
}
