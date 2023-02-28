
using wordleboard.Models;

namespace wordleboard.ViewModels
{
    public class BoardViewModel
    {
        public Board Board;
        public List<Models.UserWordle> Wordles { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public int Days { get; set; }

        public BoardViewModel(Board board, List<ApplicationUser> users, List<UserWordle> wordles, int days)
        {
            Board = board;
            Users = users;
            Wordles = wordles;
            Days = days;
        }

        public BoardViewModel(Board board)
        {
            Board = board;
        }
    }
}
