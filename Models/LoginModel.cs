using System.ComponentModel.DataAnnotations;

namespace TopCon.Api.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; }
        public bool RememberMe{ get; set; }
    }

}
