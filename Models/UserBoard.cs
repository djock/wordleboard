using System.ComponentModel.DataAnnotations;

namespace wordleboard.Models
{
    public class UserBoard
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int BoardId { get; set; }
        public string? BoardName { get; set; }
        public int DaysCount { get; set; }

        public UserBoard(string userId, int boardId)
        {
            UserId = userId; BoardId = boardId;
        }

        public override string ToString()
        {
            return $"User: {UserId}, BoardId: {BoardId}, BoardName: {BoardName}";
        }
    }
}
