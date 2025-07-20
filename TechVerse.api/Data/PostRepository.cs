using Microsoft.EntityFrameworkCore;
using TechVerse.Api.Models;
using TechVerse.Api.Dtos;


namespace TechVerse.Api.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<PostRepository> _logger;
        public PostRepository(DataContext context, ILogger<PostRepository> logger)
        {
            _logger = logger;

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
            var posts = await _context.Posts
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            return posts;
        }

        public async Task<bool> LikePost(int userId, int postId)
        {
            var existingLike = await _context.Likes.FirstOrDefaultAsync(l => l.UserId == userId && l.PostId == postId);

            if (existingLike != null)
            {
                _logger.LogInformation("Beğeni zaten var, geri alınıyor. userId: {UserId}, postId: {PostId}", userId, postId);
                _context.Likes.Remove(existingLike);
            }
            else
            {
                _logger.LogInformation("Yeni beğeni ekleniyor. userId: {UserId}, postId: {PostId}", userId, postId);
                var postExists = await _context.Posts.AnyAsync(p => p.Id == postId);
                if (!postExists)
                {
                    _logger.LogWarning("Beğeni eklenemedi çünkü postId: {PostId} veritabanında bulunamadı.", postId);
                    return false; // Gönderi bulunamadıysa işlem başarısız.
                }

                var newLike = new Like
                {
                    UserId = userId,
                    PostId = postId
                };
                await _context.Likes.AddAsync(newLike);
            }

            var success = await _context.SaveChangesAsync() > 0;
            _logger.LogInformation("SaveChangesAsync sonucu: {Success}", success);
            return success;
        }

        public async Task<Comment?> AddComment(int userId, int postId, CommentForCreationDto commentDto)
        {
            var postExists = await _context.Posts.AnyAsync(p => p.Id == postId);
            if (!postExists)
            {
                return null;
            }

            var commentToCreate = new Comment
            {
                Content = commentDto.Content,
                UserId = userId,
                PostId = postId,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Comments.AddAsync(commentToCreate);
            await _context.SaveChangesAsync();

            return commentToCreate;
        }

        public async Task<IEnumerable<Post>> GetTimeLinePosts(int userId)
        {
            var followingUserIds = await _context.Follows.Where(f => f.FollowerId == userId)
                .Select(f => f.FollowingId)
                .ToListAsync();
            followingUserIds.Add(userId);
            var timelinePosts = await _context.Posts.
            Where(p => followingUserIds.
            Contains(p.UserId)).
            Include(p => p.User).
            OrderByDescending(p => p.CreatedAt).
            ToListAsync();
            return timelinePosts;
        }

    }

}