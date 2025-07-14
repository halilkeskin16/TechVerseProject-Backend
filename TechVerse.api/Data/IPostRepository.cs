using TechVerse.Api.Models;

namespace TechVerse.Api.Data
{
    public interface IPostRepository
    {
        Task<Post> CreatePost(Post post);
        Task<Post?> GetPostById(int postId);
        Task<IEnumerable<Post>> GetPosts();
    }
}