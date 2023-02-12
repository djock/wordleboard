using wordleboard.Migrations;

namespace wordleboard.ViewModels
{
    public class BoardViewModel
    {
        public string BoardName;
        public List<UserWordle> Wordles { get; set; }
        public BoardViewModel() { }
        public BoardViewModel(string boardName, List<UserWordle> wordles)
        {
            BoardName = boardName;
            Wordles = wordles;
        }
    }
}
