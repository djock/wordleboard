using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using wordleboard.Models;
using wordleboard.Services;
using wordleboard.ViewModels;
using wordleboard.Web;

namespace wordleboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordleService _wordleService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IWordleService wordleService, UserManager<ApplicationUser> userManager, ILogger<HomeController> logger)
        {
            _wordleService = wordleService;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.GetUserAsync(User).Result;

                if (user == null)
                {
                    return RedirectToAction("Welcome");
                }

                var todayWordleResult = _wordleService.GetTodayWordleForUser(user.Id);
                string todayCookie = HttpContext.Request.Cookies[$"wordle_{AppUtils.TodayWordleId}"];

                var todayWordleViewModel = new TodayWordleViewModel(todayWordleResult, todayCookie);

                return View(todayWordleViewModel);
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