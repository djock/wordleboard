using wordleboard.Models;

namespace wordleboard.Web;

public interface IDictionaryRepository
{
    Task<WordDefinition> GetWordDefinition(string word);
}