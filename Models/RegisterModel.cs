using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TopCon.Api.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [MinLength(6, ErrorMessage = "O nome do usuário deve ter no mínimo 3 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A confirmação da senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A confirmação da senha deve ter no mínimo 6 caracteres.")]
        public string ConfirmPassword { get; set; }
    }


}
