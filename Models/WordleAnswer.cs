namespace wordleboard.Models
{
    public class WordleAnswer
    {
        public long Day { get; set; }
        public int WordleId { get; set; }
        public string? Answer { get; set; } = "";

        public WordleAnswer(long day, int wordleId, string? answer)
        {
            Day = day;
            WordleId = wordleId;
            Answer = answer;
        }
    }
}
