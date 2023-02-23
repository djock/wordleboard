namespace wordleboard.Web
{
    public interface IWordleResultsRepository
    {
        Task<string> GetToday();
        Task<string> GetAll();
    }
}
