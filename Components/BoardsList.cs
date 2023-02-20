using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using wordleboard.Models;

namespace wordleboard.Components
{
    public class BoardsList : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBoardRepository _boardRepo;

        public BoardsList(IBoardRepository boardRepo, UserManager<ApplicationUser> userManager)
        {
            _boardRepo = boardRepo;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(bool showActive)
        {
            List<Board> boards = new List<Board>();

            var user = _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User).Result;
            if (showActive)
            {
                boards = _boardRepo.AllBoardsForUser(user.Id).Where(b => b.IsActive()).ToList();
            }
            else
            {
                boards = _boardRepo.AllBoardsForUser(user.Id);
            }

            return View(boards);
        }
    }
}
