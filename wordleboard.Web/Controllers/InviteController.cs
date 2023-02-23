using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using wordleboard.Models;
using wordleboard.ViewModels;
using wordleboard.Web;

namespace wordleboard.Controllers
{
    public class InviteController : Controller
    {
        private readonly IBoardRepository _boardRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public InviteController(IBoardRepository boardRepo, UserManager<ApplicationUser> userManager)
        {
            _boardRepo = boardRepo;
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                // silent check to see if the user is already in this board
                var usersIdList = _boardRepo.GetAllUsersInBoard(id);
                var user = _userManager.GetUserAsync(User).Result;

                if (usersIdList.Contains(user.Id))
                {
                    return RedirectToAction("Index", "Home");
                }
                var board = _boardRepo.GetById(id);

                if (board == null)
                {
                    return NoContent();
                }

                var boardViewModel = new BoardViewModel(board);

                return View(boardViewModel);
            }
            else
            {
                return RedirectToAction("Welcome", "Home");
            }
        }

        public IActionResult Accept(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.GetUserAsync(User).Result;

                _boardRepo.AddUserToBoard(id, user.Id);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Welcome", "Home");
            }
        }

        public IActionResult Decline()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Welcome", "Home");
            }
        }
    }
}
