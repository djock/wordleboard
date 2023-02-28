using wordleboard.Models;

namespace wordleboard.Services
{
    public interface IWordleService
    {
        UserWordle GetTodayWordleForUser(string userId);
        List<UserWordle> GetBoardWordlesForUsers(Board board, List<string> userIds);
    }
}
