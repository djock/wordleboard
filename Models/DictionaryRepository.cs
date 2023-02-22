using System.Text.Json;
using Newtonsoft.Json;

namespace wordleboard.Models;

public class DictionaryRepository : IDictionaryRepository
{
    private readonly HttpClient _httpClient;

    public DictionaryRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<WordDefinition> GetWordDefinition(string word)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api.dictionaryapi.dev/api/v2/entries/en/" + word),
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
            List<WordDefinition> todayWordleResult = new List<WordDefinition>();

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                
                todayWordleResult = JsonConvert.DeserializeObject<List<WordDefinition>>(body);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return todayWordleResult.First();
        }
    }
}