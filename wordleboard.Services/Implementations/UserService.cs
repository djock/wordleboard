using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using wordleboard.Models;

namespace wordleboard.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsUserAuthenticated(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            return user != null && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated && _httpContextAccessor.HttpContext.User.FindFirstValue("sub") == userId;
        }

        public async Task<List<ApplicationUser>> GetUsersByIds(List<string> ids)
        {
            var usersModels = new List<ApplicationUser>();

            foreach (var userId in ids)
            {
                var userModel = await _userManager.FindByIdAsync(userId);

                if (userModel != null)
                {
                    usersModels.Add(userModel);
                }
            }

            return usersModels;
        }

        public ApplicationUser ActiveUser => _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result as ApplicationUser;
    }
}
