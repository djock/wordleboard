using wordleboard.Models;

namespace wordleboard.Web
{
    public interface IWordleRepository
    {
        List<UserWordle> AllWordlesForUser(string userId);
        UserWordle? GetById(int id);

        UserWordle? GetByIdForUser(int id, string userId);

        void AddWordle(UserWordle wordle);



        void UpdateWordle(UserWordle wordle);
    }
}
