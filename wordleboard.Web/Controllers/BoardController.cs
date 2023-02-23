using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using wordleboard.Models;
using wordleboard.ViewModels;
using wordleboard.Web;

namespace wordleboard.Controllers
{
    public class BoardController : Controller
    {
        private readonly IBoardRepository _boardRepo;
        private readonly IWordleRepository _wordleRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public BoardController(IBoardRepository boardRepo, IWordleRepository wordleRepo, UserManager<ApplicationUser> userManager)
        {
            _boardRepo = boardRepo;
            _wordleRepo = wordleRepo;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int boardId)
        {
            var usersIdList = _boardRepo.GetAllUsersInBoard(boardId);
            var board = _boardRepo.GetById(boardId);
            List<UserWordle> userWordles = new List<UserWordle>();

            if (board == null)
            {
                return NoContent();
            }

            List<ApplicationUser> usersModels = new List<ApplicationUser>();

            foreach (var userId in usersIdList)
            {
                var userModel = await _userManager.FindByIdAsync(userId);

                if (userModel != null)
                {
                    usersModels.Add(userModel);
                }
            }

            List<UserWordle> userResults = new();

            Console.WriteLine("Users count: " + boardId);

            var startDate = AppUtils.GetWordleDayFromTimestamp(board.StartDate);

            foreach (var user in usersIdList)
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

            var boardViewModel = new BoardViewModel(board, usersModels, userWordles, board.DaysCount);

            return View(boardViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DateTime selectedDate, string boardName, string boardDescription, int daysCount)
        {
            long secondsSinceEpoch = (long)(selectedDate - new DateTime(1970, 1, 1)).TotalSeconds;

            var user = _userManager.GetUserAsync(User).Result;
            var applicationUser = user as ApplicationUser;

            var board = new Board
            {
                BoardName = boardName,
                BoardDescription = boardDescription,
                StartDate = secondsSinceEpoch,
                DaysCount = daysCount,
            };


            _boardRepo.AddBoard(board, applicationUser);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult List()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("Welcome");
        }
    }
}
