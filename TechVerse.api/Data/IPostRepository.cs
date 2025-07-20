using TechVerse.Api.Models;
using TechVerse.Api.Dtos;

namespace TechVerse.Api.Data
{
    public interface IPostRepository
    {
        Task<Post> CreatePost(Post post);
        Task<Post?> GetPostById(int postId);
        Task<IEnumerable<Post>> GetPosts();
        Task<bool> LikePost(int postId, int userId);
        Task<Comment?> AddComment(int postId, int userId, CommentForCreationDto commentDto);
        Task<IEnumerable<Post>> GetTimeLinePosts(int userId);
    }
}