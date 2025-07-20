using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechVerse.Api.Data; 

namespace TechVerse.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("me")] // api/users/me
        public async Task<IActionResult> GetMyProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim.Value);

            var user = await _userRepo.GetUserById(userId);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
            
            return Ok(user);
        }

        [HttpPost("{userIdToFollow}/follow")]
        public async Task<IActionResult> FollowUser(int userIdToFollow)
        {
            var followerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (followerIdClaim == null)
            {
                return Unauthorized();
            }

            var followerId = int.Parse(followerIdClaim.Value);

            var result = await _userRepo.FollowUser(followerId, userIdToFollow);

            if (!result)
            {
                return BadRequest("İşlem gerçekleştirilemedi.");
            }

            return Ok(new { message = "İşlem başarılı." });
        }
    }
}