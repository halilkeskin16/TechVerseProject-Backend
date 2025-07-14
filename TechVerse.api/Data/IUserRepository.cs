using TechVerse.Api.Models;

namespace TechVerse.Api.Data
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByUsername(string username);

        Task<IEnumerable<User>> GetAllUsers();

    }
}