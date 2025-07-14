using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechVerse.Api.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(280)]
        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public ICollection<Like> Likes { get; set; } = new List<Like>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}