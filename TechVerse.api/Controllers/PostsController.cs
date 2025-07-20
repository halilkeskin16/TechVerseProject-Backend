using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechVerse.Api.Data;
using TechVerse.Api.Dtos;
using TechVerse.Api.Models;

namespace TechVerse.Api.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepo;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IPostRepository postRepo, ILogger<PostsController> logger)
        {
            _postRepo = postRepo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostForCreationDto postForCreationDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Kullanıcı kimliği bulunamadı.");
            }

            var userId = int.Parse(userIdClaim.Value);

            var postToCreate = new Post
            {
                Content = postForCreationDto.Content,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            var createdPost = await _postRepo.CreatePost(postToCreate);

            return Ok(createdPost);

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var postsFromRepo = await _postRepo.GetPosts();
            var postsToReturn = postsFromRepo.Select(p => new PostForListDto
            {
                Id = p.Id,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                User = p.User != null ? new UserForListDto
                {
                    Id = p.User.Id,
                    Username = p.User.Username,
                    DisplayName = p.User.DisplayName,
                    ProfileImageUrl = p.User.ProfileImageUrl
                } : null
            });
            return Ok(postsToReturn);
        }
        [HttpPost("{postId}/like")]
        public async Task<IActionResult> LikePost(int postId)
        {
            _logger.LogInformation("LikePost endpoint'i çağrıldı. Gelen postId: {PostId}", postId);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                _logger.LogWarning("Token'da NameIdentifier (userId) claim'i bulunamadı.");
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim.Value);

            _logger.LogInformation("Token'dan okunan userId: {UserId}", userId);

            var result = await _postRepo.LikePost(userId, postId);

            if (!result)
            {
                _logger.LogWarning("PostRepository.LikePost metodu 'false' döndürdü. postId: {PostId}, userId: {UserId}", postId, userId);
                return BadRequest("İşlem gerçekleştirilemedi.");
            }

            _logger.LogInformation("İşlem başarılı. postId: {PostId}, userId: {UserId}", postId, userId);
            return Ok();
        }

        [HttpPost("{postId}/comments")]
        public async Task<IActionResult> AddComment(int postId, CommentForCreationDto commentForCreationDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim.Value);
            var createdComment = await _postRepo.AddComment(userId, postId, commentForCreationDto);
            if (createdComment == null)
            {
                return NotFound("Yorum yapılacak gönderi bulunamadı.");
            }
            return Ok(createdComment);
        }
        [HttpGet("timeline")] // api/posts/timeline
        public async Task<IActionResult> GetTimeLine()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim.Value);
            var timelinePosts= await _postRepo.GetTimeLinePosts(userId);

            var postsToReturn = timelinePosts.Select(p => new PostForListDto
            {
                Id = p.Id,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                User = p.User != null ? new UserForListDto
                {
                    Id = p.User.Id,
                    Username = p.User.Username,
                    DisplayName = p.User.DisplayName,
                    ProfileImageUrl = p.User.ProfileImageUrl
                } : null
            });
            return Ok(postsToReturn);
        }
    }
}

