namespace TechVerse.Api.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? DisplayName { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}