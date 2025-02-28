using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TopCon.Api.Entities
{
    public class Posts
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public IdentityUser CreatedBy { get; set; } 
    }
}
