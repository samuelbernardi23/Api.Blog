using System.ComponentModel.DataAnnotations;

namespace TopCon.Api.Models
{
    public class UpdatePostModel
    {
        [Required]
        [MaxLength(80, ErrorMessage = "Título da Postagem não pode ultrapassar 80 caracteres.")]
        [MinLength(10, ErrorMessage = "O título da postagem deve conter pelo menos 10 caracteres")]
        public string Title { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "O conteúdo da postagem deve conter pelo menos 10 caracteres")]
        [MaxLength(400, ErrorMessage = "Contéudo da Postagem não pode ultrapassar 400 caracteres.")]
        public string Content { get; set; }
    }
}
