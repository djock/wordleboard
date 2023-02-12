using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using wordleboard.Models;
using wordleboard.Utils;
using wordleboard.ViewModels;

namespace wordleboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBoardRepository _boardRepo;
        private readonly IWordleRepository _wordleRepo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IBoardRepository userBoardRepository, IWordleRepository userWordleRepository, UserManager<ApplicationUser> userManager, ILogger<HomeController> logger)
        {
            _boardRepo = userBoardRepository;
            _wordleRepo = userWordleRepository;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.GetUserAsync(User).Result;

                UserWordle userWordle = new UserWordle { WordleId = AppUtils.TodayWordleId, UserId = user.Id };
                //userWordle.UserId = user.Id;
                //userWordle.WordleId = AppUtils.TodayWordleId;

                var todayWordle = _wordleRepo.AllWordlesForUser(user.Id).FirstOrDefault(w => w.WordleId == AppUtils.TodayWordleId);

                if (todayWordle != null)
                {
                    userWordle = todayWordle;
                }

                var userBoardsViewModel = new UserBoardsViewModel(user, _boardRepo.AllBoardsForUser(user.Id).ToList(), userWordle);

                return View(userBoardsViewModel);
            }

            return RedirectToAction("Welcome");
        }

        [HttpPost]
        public IActionResult Index(int points, int bonus)
        {
            var user = _userManager.GetUserAsync(User).Result;

            UserWordle userWordle = new UserWordle
            {
                WordleId = AppUtils.TodayWordleId,
                UserId = user.Id,
                Points = points,
                Bonus = bonus
            };

            if (_wordleRepo.AllWordlesForUser(user.Id).Exists(x => x.WordleId == userWordle.WordleId && x.UserId == userWordle.UserId))
            {
                _wordleRepo.UpdateWordle(userWordle);
            }
            else
            {
                _wordleRepo.AddWordle(userWordle);
            }

            return Index();
        }

        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult CreateBoard()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBoard(DateTime selectedDate, string boardName, string boardDescription, int daysCount)
        {
            long secondsSinceEpoch = (long)(selectedDate - new DateTime(1970, 1, 1)).TotalSeconds;

            var user = _userManager.GetUserAsync(User).Result;
            var applicationUser = user as ApplicationUser;

            var board = new Board
            {
                BoardName = boardName,
                BoardDescription = boardDescription,
                BoardId = _boardRepo.BoardCount,
                StartDate = secondsSinceEpoch,
                DaysCount = daysCount,
            };


            _boardRepo.AddBoard(board, applicationUser);

            return Index();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}