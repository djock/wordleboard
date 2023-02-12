using wordleboard.Models;

namespace wordleboard.ViewModels
{
    public class BoardViewModel
    {
        public Board Board;
        //public List<Models.UserWordle> Wordles { get; set; }
        public List<string> Users { get; set; }
        public BoardViewModel() { }

        public BoardViewModel(Board board, List<string> users)
        {
            Board = board;
            Users = users;

            Console.WriteLine("BoardViewModel " + board.ToString());
        }
    }
}
