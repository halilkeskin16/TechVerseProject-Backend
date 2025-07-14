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
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("me")] // api/users/me
        public async Task<IActionResult> GetMyProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("Kullanıcı kimliği bulunamadı veya yetkisiz erişim");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest("Geçersiz kullanıcı kimliği");
            }

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            return Ok(user);
        
        }
    }
}