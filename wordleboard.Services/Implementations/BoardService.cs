using wordleboard.Db;
using wordleboard.Models;

namespace wordleboard.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepo;

        public BoardService(IBoardRepository boardRepo)
        {
            _boardRepo = boardRepo;
        }

        public Board GetBoard(int boardId) => _boardRepo.GetById(boardId);

        public List<string> GetAllUsersInBoard(int boardId) => _boardRepo.GetAllUsersInBoard(boardId);

        public void CreateBoard(string boardName, string boardDescription, int daysCount, DateTime selectedDate, ApplicationUser user)
        {
            long secondsSinceEpoch = (long)(selectedDate - new DateTime(1970, 1, 1)).TotalSeconds;

            var board = new Board
            {
                BoardName = boardName,
                BoardDescription = boardDescription,
                StartDate = secondsSinceEpoch,
                DaysCount = daysCount,
            };

            _boardRepo.AddBoard(board, user);
        }
    }
}
