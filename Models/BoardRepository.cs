namespace wordleboard.Models
{
    public class BoardRepository : IBoardRepository
    {
        private readonly WordleBoardDbContext _dbContext;

        public int BoardCount => _dbContext.Boards.Count();

        public BoardRepository(WordleBoardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Board> AllBoardsForUser(string userId) => _dbContext.BoardUsers
                                                                        .Where(bu => bu.UserId == userId)
                                                                        .Select(bu => bu.Board)
                                                                        .ToList();

        public void AddBoard(Board board, ApplicationUser user)
        {
            _dbContext.Boards.Add(board);

            var boardUsers = new List<BoardUser>
            {
                new BoardUser { UserId = user.Id, BoardId = board.Id, Board = board, User = user}
            };

            _dbContext.BoardUsers.AddRange(boardUsers);

            _dbContext.SaveChanges();
        }

        public void RemoveBoard(Board board)
        {
            _dbContext.Boards.Remove(board);
            _dbContext.SaveChanges();
        }

        public void AddUserToBoard(int boardId, string userId)
        {
        }

        public Board GetById(int boardId)
        {
            var board = _dbContext.Boards.FirstOrDefault(bu => bu.BoardId == boardId);

            if (board == null)
            {
                Console.WriteLine("Board Not found");
            }

            return board;
        }

        public List<string> GetAllUsersInBoard(int boardId)
        {

            return _dbContext.BoardUsers
                                    .Where(bu => bu.BoardId == boardId)
                                    .Select(bu => bu.User)
                                    .Select(u => u.Id)
                                    .ToList();
        }
    }
}
