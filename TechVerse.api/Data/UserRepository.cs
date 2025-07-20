using Microsoft.EntityFrameworkCore;
using TechVerse.Api.Models;

namespace TechVerse.Api.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }

        public async Task<bool> FollowUser(int followerId, int userIdToFollow)
        {
            if (followerId == userIdToFollow)
            {
                return false;
            }

            var existingFollow = await _context.Follows.FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowingId == userIdToFollow);
            if (existingFollow != null)
            {
                _context.Follows.Remove(existingFollow);
            }

            else
            {
                var userToFollowExists = await _context.Users.AnyAsync(u => u.Id == userIdToFollow);
                if (!userToFollowExists)
                {
                    return false;
                }

                var newFollow = new Follow
                {
                    FollowerId = followerId,
                    FollowingId = userIdToFollow
                };
                await _context.Follows.AddAsync(newFollow);
            }
            
            return await _context.SaveChangesAsync() > 0;

        }
    }
}