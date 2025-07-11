using System.ComponentModel.DataAnnotations;

namespace TechVerse.Api.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(280)]
        public string? Content { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int PostId { get; set; }
        public Post? Post { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}