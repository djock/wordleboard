using System.ComponentModel.DataAnnotations;

namespace wordleboard.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        public int BoardId { get; set; }
        public string? BoardName { get; set; }
        public string? BoardDescription { get; set; }
        public int DaysCount { get; set; }
        public long StartDate { get; set; }
        public virtual ICollection<BoardUser> BoardUsers { get; set; }

        public override string ToString()
        {
            return $"User: BoardId: {BoardId}, BoardName: {BoardName}, StartDate: {StartDate}";
        }
    }
}
