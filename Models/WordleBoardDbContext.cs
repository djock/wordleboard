using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace wordleboard.Models
{
    public class WordleBoardDbContext : IdentityDbContext
    {
        public WordleBoardDbContext(DbContextOptions<WordleBoardDbContext> options) : base(options)
        {
        }
    }
}
