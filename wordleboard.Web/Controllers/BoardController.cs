using Microsoft.AspNetCore.Mvc;
using wordleboard.Services;
using wordleboard.ViewModels;

namespace wordleboard.Controllers
{
    public class BoardController : Controller
    {
        private IBoardService _boardService;
        private IWordleService _wordleService;
        private IUserService _userService;

        public BoardController(IBoardService boardService, IUserService userService, IWordleService wordleService)
        {
            _userService = userService;
            _wordleService = wordleService;
            _boardService = boardService;
        }

        public async Task<IActionResult> Index(int boardId)
        {
            var board = _boardService.GetBoard(boardId);

            if (board == null)
            {
                return NoContent();
            }

            var usersIdList = _boardService.GetAllUsersInBoard(boardId);
            var users = await _userService.GetUsersByIds(usersIdList);
            var usersWordles = _wordleService.GetBoardWordlesForUsers(board, usersIdList);

            var boardViewModel = new BoardViewModel(board, users, usersWordles, board.DaysCount);

            return View(boardViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DateTime selectedDate, string boardName, string boardDescription, int daysCount)
        {
            var applicationUser = _userService.ActiveUser;

            _boardService.CreateBoard(boardName, boardDescription, daysCount, selectedDate, applicationUser);

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
