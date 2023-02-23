using System.Text.Json;

namespace wordleboard.Models
{
    public class WordleResultsRepository : IWordleResultsRepository
    {
        private readonly HttpClient _httpClient;

        public WordleResultsRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
            using (var response = await _httpClient.SendAsync(request))
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
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var todayWordleResult = JsonSerializer.Deserialize<TodayWordleResult>(body);
                return todayWordleResult.today;
            }
        }
    }
}
