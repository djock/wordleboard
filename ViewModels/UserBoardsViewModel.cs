using Microsoft.AspNetCore.Identity;
using wordleboard.Models;

namespace wordleboard.ViewModels
{
    [Serializable]
    public class UserBoardsViewModel
    {
        public IdentityUser? User { get; set; }
        public List<Board> UserBoards { get; set; }
        public UserWordle TodayWordle { get; set; }

        public UserBoardsViewModel() { }

        public UserBoardsViewModel(IdentityUser user, List<Board> boards, UserWordle todayWordle)
        {
            User = user;
            UserBoards = boards;
            TodayWordle = todayWordle;
        }
    }
}
