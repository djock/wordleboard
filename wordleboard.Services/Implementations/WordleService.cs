using wordleboard.Db;
using wordleboard.Models;
using wordleboard.Web;

namespace wordleboard.Services
{
    public class WordleService : IWordleService
    {
        private readonly IWordleRepository _wordleRepo;
        private readonly IBoardService _boardService;
        public WordleService(IWordleRepository wordleRepo, IBoardService boardService)
        {
            _wordleRepo = wordleRepo;
            _boardService = boardService;
        }

        public int TodayWordleId => DateTime.Now.Subtract(new DateTime(2021, 6, 19)).Days;
        public DateTime WordleStartDate = new DateTime(2021, 6, 19);

        public UserWordle GetTodayWordleForUser(string userId)
        {
            UserWordle userWordle = new UserWordle { WordleId = TodayWordleId, UserId = userId };

            var todayWordle = _wordleRepo.AllWordlesForUser(userId).FirstOrDefault(w => w.WordleId == TodayWordleId);

            if (todayWordle != null)
            {
                userWordle = todayWordle;
            }
            else
            {
                Console.WriteLine("Can't find score for today");
            }
            return userWordle;
        }

        public List<UserWordle> GetBoardWordlesForUsers(Board board, List<string> userIds)
        {
            var startDate = AppUtils.GetWordleDayFromTimestamp(board.StartDate);
            List<UserWordle> userWordles = new();


            foreach (var user in userIds)
            {
                if (board.DaysCount > 0)
                {
                    var endDate = startDate + board.DaysCount - 1;

                    var maxDay = endDate <= AppUtils.TodayWordleId ? endDate : AppUtils.TodayWordleId;

                    for (int i = startDate; i <= maxDay; i++)
                    {
                        var wordle = _wordleRepo.GetByIdForUser(i, user);
                        userWordles.Add(wordle);
                    }
                }
                else
                {
                    for (var i = startDate; i <= AppUtils.TodayWordleId; i++)
                    {
                        var wordle = _wordleRepo.GetByIdForUser(i, user);
                        userWordles.Add(wordle);
                    }
                }
            }

            return userWordles;
        }
    }
}
