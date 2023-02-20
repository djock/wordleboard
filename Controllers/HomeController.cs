using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using wordleboard.Models;
using wordleboard.Utils;

namespace wordleboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWordleRepository _wordleRepo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IWordleRepository userWordleRepository, UserManager<ApplicationUser> userManager, ILogger<HomeController> logger)
        {
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

                var todayWordle = _wordleRepo.AllWordlesForUser(user.Id).FirstOrDefault(w => w.WordleId == AppUtils.TodayWordleId);

                if (todayWordle != null)
                {
                    userWordle = todayWordle;
                }
                else
                {
                    Console.WriteLine("Can't find answer for today");
                }

                return View(userWordle);
            }

            return RedirectToAction("Welcome");
        }

        public IActionResult Welcome()
        {
            return View();
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