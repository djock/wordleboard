namespace wordleboard.Models;

public interface IDictionaryRepository
{
    Task<WordDefinition> GetWordDefinition(string word);
}