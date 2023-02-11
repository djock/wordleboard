namespace wordleboard.Models
{
    public class BoardRepository : IBoardRepository
    {
        private readonly WordleBoardDbContext _dbContext;

        public BoardRepository(WordleBoardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserBoard> AllBoards => _dbContext.UserBoards.ToList();

        public void Add(UserBoard board)
        {
            _dbContext.UserBoards.Add(board);
            _dbContext.SaveChanges();
        }

        public UserBoard GetById(int id) => _dbContext.UserBoards.FirstOrDefault(b => b.Id == id);
    }
}
