using System.Text.Json;
using wordleboard.Models;

namespace wordleboard.Services
{
    public class WordleAnswersService : IWordleAnswersService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WordleAnswersService(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }

        public async Task<string> GetAll()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://wordle-answers-solutions.p.rapidapi.com/answers"),
                Headers =
    {
        { "X-RapidAPI-Key", "LSWtujEIqOmshvCJuaNmWIT8DY4sp1wefUzjsntbbYMc7LgNiD" },
        { "X-RapidAPI-Host", "wordle-answers-solutions.p.rapidapi.com" },
    },
            };

            var httpClient = _httpClientFactory.CreateClient();

            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return body;
            }
        }

        public async Task<string> GetToday()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://wordle-answers-solutions.p.rapidapi.com/today"),
                Headers =
    {
        { "X-RapidAPI-Key", "LSWtujEIqOmshvCJuaNmWIT8DY4sp1wefUzjsntbbYMc7LgNiD" },
        { "X-RapidAPI-Host", "wordle-answers-solutions.p.rapidapi.com" },
    },
            };

            var httpClient = _httpClientFactory.CreateClient();

            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var todayWordleResult = JsonSerializer.Deserialize<TodayWordleResult>(body);
                return todayWordleResult.today;
            }
        }
    }
}
