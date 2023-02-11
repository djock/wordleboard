using System.ComponentModel.DataAnnotations;

namespace wordleboard.Models
{
    public class UserWordle
    {
        [Key]
        public int Id { get; set; }
        public int WordleId { get; set; }
        public string? UserId { get; set; }
        [Required]
        public int Points { get; set; }
        public int Bonus { get; set; }

        public override string ToString()
        {
            {
                return $"UserWordle: {Id} {WordleId} {UserId} {Points} {Bonus}";
            }
        }
    }
}
