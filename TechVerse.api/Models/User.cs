using System.ComponentModel.DataAnnotations;

namespace TechVerse.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string? Username { get; set; } // Giriş için kullanılan, benzersiz ad

        [Required]
        [EmailAddress]
        public string? Email { get; set; } // Benzersiz e-posta

        [Required]
        public string? PasswordHash { get; set; }

        [StringLength(50)]
        public string? DisplayName { get; set; } // Ekranda görünecek ad (örn: Halil Keskin)

        [StringLength(160)]
        public string? Bio { get; set; } // Kullanıcı biyografisi

        public string? ProfileImageUrl { get; set; } // Profil fotoğrafının URL'i
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // Bu kullanıcının gönderileri
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        
        // Bu kullanıcının yaptığı beğeniler
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        
        // Bu kullanıcının yaptığı yorumlar
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        
        // Bu kullanıcının takip ettikleri (Following)
        public ICollection<Follow> Following { get; set; } = new List<Follow>();

        // Bu kullanıcıyı takip edenler (Followers)
        public ICollection<Follow> Followers { get; set; } = new List<Follow>();
    }
}