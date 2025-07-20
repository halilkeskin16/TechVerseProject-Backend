using Microsoft.EntityFrameworkCore;
using TechVerse.Api.Models;

namespace TechVerse.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follow> Follows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Follow tablosu için bileşik bir anahtar anahtar tanımlıyoruz.
            // (Takip Eden + Takip Edilen) kombinasyonu benzersiz olmalı.
            modelBuilder.Entity<Follow>()
                .HasKey(f => new { f.FollowerId, f.FollowingId });

            // 'Follower' ilişkisini tanımlıyoruz.
            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Follower)
                .WithMany(u => u.Following) // Bir kullanıcının takip ettikleri
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict); // Bir kullanıcı silinirse bu ilişkiyi silme

            // 'Following' ilişkisini tanımlıyoruz.
            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Following)
                .WithMany(u => u.Followers) // Bir kullanıcıyı takip edenler
                .HasForeignKey(f => f.FollowingId)
                .OnDelete(DeleteBehavior.Restrict); // Bir kullanıcı silinirse bu ilişkiyi silme
        }
    }
}