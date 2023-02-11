namespace wordleboard.Models
{
    public interface IWordleRepository
    {
        List<UserWordle> AllWordlesForUser(string userId);
        UserWordle? GetById(int id);

        void AddWordle(UserWordle wordle);

        void UpdateWordle(UserWordle wordle);
    }
}
