namespace wordleboard.Models
{
    public interface IBoardRepository
    {
        List<UserBoard> AllBoards { get; }
        UserBoard? GetById(int id);

        void Add(UserBoard board);
    }
}
