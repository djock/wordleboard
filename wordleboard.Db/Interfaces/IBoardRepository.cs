using wordleboard.Models;

namespace wordleboard.Db
{
    public interface IBoardRepository
    {
        int BoardCount { get; }
        List<Board> AllBoardsForUser(string userId);
        Board? GetById(int id);

        void AddBoard(Board board, ApplicationUser user);

        void AddUserToBoard(int boardId, string userId);

        List<string> GetAllUsersInBoard(int boardId);
    }
}
