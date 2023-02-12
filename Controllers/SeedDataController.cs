using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using wordleboard.Models;
using wordleboard.Utils;

namespace wordleboard.Controllers
{
    public class SeedDataController : Controller
    {
        private readonly WordleBoardDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedDataController(WordleBoardDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult PopulateForI()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.GetUserAsync(User).Result;
                var applicationUser = user as ApplicationUser;

                WordleDbInitializer.SeedDataForI(_context, applicationUser.Id);

                return View();
            }

            return RedirectToAction("Welcome", "Home");
        }
    }
}
