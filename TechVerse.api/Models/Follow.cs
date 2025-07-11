using System.ComponentModel.DataAnnotations.Schema;

namespace TechVerse.Api.Models
{
    public class Follow
    {
        // Takip eden kişi
        public int FollowerId { get; set; }
        public User? Follower { get; set; }

        // Takip edilen kişi
        public int FollowingId { get; set; }
        public User? Following { get; set; }
    }
}