namespace wordleboard.Models
{
    public class BoardUser
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public string UserId { get; set; }
        public virtual Board Board { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
