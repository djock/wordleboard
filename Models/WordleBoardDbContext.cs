using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace wordleboard.Models
{
    public class WordleBoardDbContext : IdentityDbContext
    {
        public WordleBoardDbContext(DbContextOptions<WordleBoardDbContext> options) : base(options)
        {
        }

        public DbSet<UserBoard> UserBoards { get; set; }
        public DbSet<UserWordle> UserWordles { get; set; }
    }
}
