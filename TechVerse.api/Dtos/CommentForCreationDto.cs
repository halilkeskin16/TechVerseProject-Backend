using System.ComponentModel.DataAnnotations;

namespace TechVerse.Api.Dtos
{
    public class CommentForCreationDto
    {
        [Required(ErrorMessage = "Yorum içeriği boş olamaz.")]
        [StringLength(280, ErrorMessage = "Yorum en fazla 280 karakter olabilir.")]
        public string? Content { get; set; }
    }
}