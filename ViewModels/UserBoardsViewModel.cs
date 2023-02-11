using Microsoft.AspNetCore.Identity;
using wordleboard.Models;

namespace wordleboard.ViewModels
{
    public class UserBoardsViewModel
    {
        public IdentityUser? User { get; set; }
        public List<UserBoard> UserBoards { get; set; }
        public UserWordle TodayWordle { get; set; }

        public UserBoardsViewModel() { }

        public UserBoardsViewModel(IdentityUser user, List<UserBoard> boards, UserWordle todayWordle)
        {
            User = user;
            UserBoards = boards;
            TodayWordle = todayWordle;
        }
    }
}
