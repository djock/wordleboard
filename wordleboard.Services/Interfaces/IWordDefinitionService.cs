using wordleboard.Models;

namespace wordleboard.Services;

public interface IWordDefinitionService
{
    Task<WordDefinition> GetWordDefinition(string word);
}