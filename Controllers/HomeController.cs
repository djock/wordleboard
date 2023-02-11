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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBoardRepository _boardRepo;
        private readonly IWordleRepository _wordleRepo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IBoardRepository userBoardRepository, IWordleRepository userWordleRepository, UserManager<IdentityUser> userManager, ILogger<HomeController> logger)
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

                var todayWordle = _wordleRepo.AllWordlesForUser(user.Id).Where(w => w.UserId == user.Id).ToList().OrderByDescending(w => w.WordleId);

                if (todayWordle.Count() > 0)
                {
                    userWordle = todayWordle.First();
                }

                var userBoardsViewModel = new UserBoardsViewModel(user, _boardRepo.AllBoards.ToList(), userWordle);

                return View(userBoardsViewModel);
            }

            return RedirectToAction("Welcome");
        }

        [HttpPost]
        public IActionResult Index(UserBoardsViewModel obj)
        {
            Console.WriteLine($"Post {obj.TodayWordle.ToString()}");
            var user = _userManager.GetUserAsync(User).Result;

            if (_wordleRepo.AllWordlesForUser(user.Id).Exists(x => x.WordleId == obj.TodayWordle.WordleId && x.UserId == obj.TodayWordle.UserId))
            {
                _wordleRepo.UpdateWordle(obj.TodayWordle);
            }
            else
            {
                _wordleRepo.AddWordle(obj.TodayWordle);
            }

            return Index();
        }

        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult CreateBoard()
        {
            var user = _userManager.GetUserAsync(User).Result;

            var userBoard = new UserBoard(user.Id, _boardRepo.AllBoards.Count());

            return View(userBoard);
        }

        [HttpPost]
        public IActionResult CreateBoard(UserBoard userBoard)
        {
            Console.WriteLine(userBoard.ToString());
            //_dbContext.UserBoards.Add(userBoard);

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