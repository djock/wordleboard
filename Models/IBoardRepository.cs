namespace wordleboard.Models
{
    public interface IBoardRepository
    {
        int BoardCount { get; }
        List<Board> AllBoardsForUser(string userId);
        Board? GetById(int id);

        void AddBoard(Board board, ApplicationUser user);

        List<string> GetAllUsersInBoard(int boardId);
    }
}
