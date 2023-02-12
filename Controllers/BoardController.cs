using Microsoft.AspNetCore.Mvc;
using wordleboard.Models;

namespace wordleboard.Controllers
{
    public class BoardController : Controller
    {
        private readonly IBoardRepository _boardRepo;
        private readonly IWordleRepository _wordleRepo;

        public BoardController(IBoardRepository boardRepo, IWordleRepository wordleRepo)
        {
            _boardRepo = boardRepo;
            _wordleRepo = wordleRepo;
        }

        public IActionResult Index(int id)
        {
            var usersIdList = _boardRepo.GetAllUsersInBoard(id);

            List<UserWordle> userResults = new();

            foreach (var user in usersIdList)
            {
                //if (board.DaysCount > 0)
                //{
                //    var endDate = AppUtils.GetWordleDayFromTimestamp(board.StartDate) + board.DaysCount;

                //    var maxDay = endDate <= AppUtils.TodayWordleId ? endDate : AppUtils.TodayWordleId;

                //    for (var i = board.StartDate; i <= maxDay; i++)
                //    {
                //    }
                //}
                //else
                //{
                //    for (var i = board.StartDate; i <= board.StartDate + board.DaysCount; i++)
                //    {
                //    }

                //    foreach (var wordle in _wordleRepo.AllWordlesForUser(user))
                //    {
                //    }
                //}
            }

            return View();
        }
    }
}
