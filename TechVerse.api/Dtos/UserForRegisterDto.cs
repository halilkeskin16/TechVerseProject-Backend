using System.ComponentModel.DataAnnotations;

namespace TechVerse.Api.Dtos
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Şifre en az 6, en fazla 20 karakter olmalıdır.")]
        public string? Password { get; set; }
    }
}