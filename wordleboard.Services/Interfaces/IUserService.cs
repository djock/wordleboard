using wordleboard.Models;

namespace wordleboard.Services
{
    public interface IUserService
    {
        bool IsUserAuthenticated(string userId);

        Task<List<ApplicationUser>> GetUsersByIds(List<string> ids);
        ApplicationUser ActiveUser { get; }
    }
}
