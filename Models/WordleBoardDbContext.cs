using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace wordleboard.Models
{
    public class WordleBoardDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardUser> BoardUsers { get; set; }
        public DbSet<UserWordle> UserWordles { get; set; }

        public WordleBoardDbContext(DbContextOptions<WordleBoardDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Board>()
                .HasMany(b => b.BoardUsers)
                .WithOne(bu => bu.Board)
                .HasForeignKey(bu => bu.BoardId)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.BoardUsers)
                .WithOne(bu => bu.User)
                .HasForeignKey(bu => bu.UserId)
                .IsRequired();

            modelBuilder.Entity<BoardUser>()
                .HasKey(bu => new { bu.BoardId, bu.UserId });

            modelBuilder.Entity<BoardUser>()
                .HasOne(bu => bu.Board)
                .WithMany(b => b.BoardUsers)
                .HasForeignKey(bu => bu.BoardId);

            modelBuilder.Entity<BoardUser>()
                .HasOne(bu => bu.User)
                .WithMany(u => u.BoardUsers)
                .HasForeignKey(bu => bu.UserId);
        }
    }
}
