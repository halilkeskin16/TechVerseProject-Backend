using System.ComponentModel.DataAnnotations;

namespace TechVerse.Api.Dtos
{
    public class PostForCreationDto
    {
        [Required(ErrorMessage = "Gönderi içeriği boş olamaz.")]
        [StringLength(280, ErrorMessage = "Gönderi en fazla 280 karakter olabilir.")]
        public string? Content { get; set; }
    }
}