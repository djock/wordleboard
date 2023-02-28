using wordleboard.Models;

namespace wordleboard.Services
{
    public interface IBoardService
    {
        Board GetBoard(int boardId);
        List<string> GetAllUsersInBoard(int boardId);

        void CreateBoard(string boardName, string boardDescription, int daysCount, DateTime selectedDate, ApplicationUser user);
    }
}
