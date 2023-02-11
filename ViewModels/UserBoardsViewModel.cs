using Microsoft.AspNetCore.Identity;
using wordleboard.Models;

namespace wordleboard.ViewModels
{
    public class UserBoardsViewModel
    {
        public IdentityUser? User { get; set; }
        public List<UserBoard> UserBoards { get; set; }

        public UserBoardsViewModel(IdentityUser user, List<UserBoard> boards)
        {
            User = user;
            UserBoards = boards;
        }
    }
}
