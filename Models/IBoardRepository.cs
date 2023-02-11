namespace wordleboard.Models
{
    public interface IBoardRepository
    {
        int BoardCount { get; }
        List<UserBoard> AllBoardsForUser(string userId);
        UserBoard? GetById(int id);

        void AddBoard(UserBoard board);
    }
}
