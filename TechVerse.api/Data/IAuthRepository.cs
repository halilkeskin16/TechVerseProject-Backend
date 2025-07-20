using TechVerse.Api.Models;

namespace TechVerse.Api.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User?> Login(string usernameOrEmail, string password);
        Task<bool> UserExists(string username , string email);
    }
}