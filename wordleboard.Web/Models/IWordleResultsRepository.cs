namespace wordleboard.Models
{
    public interface IWordleResultsRepository
    {
        Task<string> GetToday();
        Task<string> GetAll();
    }
}
