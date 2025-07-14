using Microsoft.EntityFrameworkCore;
using TechVerse.Api.Models;

namespace TechVerse.Api.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;
        public PostRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<Post> CreatePost(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }
        public async Task<Post?> GetPostById(int postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            return post;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _context.Posts.OrderByDescending(p => p.CreatedAt).ToListAsync();
        }
    }
}