namespace TechVerse.Api.Dtos
{
    public class PostForListDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserForListDto? User { get; set; }
    }
}