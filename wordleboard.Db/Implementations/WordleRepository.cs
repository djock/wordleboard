using wordleboard.Models;

namespace wordleboard.Db
{
    public class WordleRepository : IWordleRepository
    {
        private readonly WordleBoardDbContext _dbContext;

        public WordleRepository(WordleBoardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserWordle> AllWordlesForUser(string userId) => _dbContext.UserWordles.Where(w => w.UserId == userId).ToList();

        public void AddWordle(UserWordle wordle)
        {
            _dbContext.UserWordles.Add(wordle);
            _dbContext.SaveChanges();
        }

        public UserWordle? GetById(int id) => _dbContext.UserWordles.FirstOrDefault(w => w.WordleId == id);

        public void UpdateWordle(UserWordle wordle)
        {
            var entry = _dbContext.UserWordles.FirstOrDefault(x => x.WordleId == wordle.WordleId && x.UserId == wordle.UserId);

            if (entry != null)
            {
                entry.Points = wordle.Points;
                entry.Bonus = wordle.Bonus;
            }

            _dbContext.SaveChanges();
        }

        public UserWordle? GetByIdForUser(int id, string userId) => _dbContext.UserWordles.FirstOrDefault(w => w.WordleId == id && w.UserId == userId);
    }
}
