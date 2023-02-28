namespace wordleboard.Services
{
    public interface IWordleAnswersService
    {
        Task<string> GetToday();
        Task<string> GetAll();
    }
}
