namespace wordleboard.Models
{
    public class BoardRepository : IBoardRepository
    {
        private readonly WordleBoardDbContext _dbContext;

        public int BoardCount => _dbContext.UserBoards.Count();

        public BoardRepository(WordleBoardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserBoard> AllBoardsForUser(string userId) => _dbContext.UserBoards.Where(b => b.UserId == userId).ToList();

        public void AddBoard(UserBoard board)
        {
            _dbContext.UserBoards.Add(board);
            _dbContext.SaveChanges();
        }

        public UserBoard GetById(int id) => _dbContext.UserBoards.FirstOrDefault(b => b.Id == id);
    }
}
