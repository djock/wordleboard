using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using wordleboard.Models;
using wordleboard.ViewModels;

namespace wordleboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly WordleBoardDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(WordleBoardDbContext dbContext, UserManager<IdentityUser> userManager, ILogger<HomeController> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                Console.WriteLine("User is authenticated");
                var user = _userManager.GetUserAsync(User).Result;

                var userBoardsViewModel = new UserBoardsViewModel(user, _dbContext.UserBoards.ToList());

                return View(userBoardsViewModel);
            }

            return RedirectToAction("Welcome");

        }

        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult CreateBoard()
        {
            var user = _userManager.GetUserAsync(User).Result;

            var userBoard = new UserBoard(user.Id, _dbContext.UserBoards.Count());

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