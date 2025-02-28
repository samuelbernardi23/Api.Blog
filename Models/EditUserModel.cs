using System.ComponentModel.DataAnnotations;

namespace TopCon.Api.Models
{
    public class EditUserModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string UserName { get; set; }
    }

}
