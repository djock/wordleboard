using wordleboard.Models;

namespace wordleboard.ViewModels
{
    public class TodayWordleViewModel
    {
        public UserWordle Wordle;
        public string Cookie;

        public TodayWordleViewModel(UserWordle wordle, string cookie)
        {
            Wordle = wordle;
            Cookie = cookie;
        }
    }
}
