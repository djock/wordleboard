using Microsoft.AspNetCore.Identity;

namespace wordleboard.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<BoardUser> BoardUsers { get; set; }
    }
}
