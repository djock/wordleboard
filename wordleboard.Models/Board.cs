using System.ComponentModel.DataAnnotations;
using wordleboard.Web;

namespace wordleboard.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        public string? BoardName { get; set; }
        public string? BoardDescription { get; set; }
        public int DaysCount { get; set; }
        public long StartDate { get; set; }
        public virtual ICollection<BoardUser> BoardUsers { get; set; }

        public Board() { }
        public override string ToString()
        {
            return $"User: BoardId: {Id}, BoardName: {BoardName}, StartDate: {StartDate}";
        }

        public bool IsActive()
        {
            if (DaysCount == 0) return true;

            var startDay = AppUtils.GetWordleDayFromTimestamp(StartDate);
            var today = AppUtils.TodayWordleId;

            var endDay = startDay + DaysCount - 1;

            return startDay <= today && today <= endDay;
        }
    }
}
