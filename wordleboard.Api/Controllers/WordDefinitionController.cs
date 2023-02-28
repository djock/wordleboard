using Microsoft.AspNetCore.Mvc;
using wordleboard.Services;

namespace wordleboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordDefinitionController : ControllerBase
    {
        private readonly IWordDefinitionService _wordDefinitionService;

        public WordDefinitionController(IWordDefinitionService wordDefinitionService)
        {
            _wordDefinitionService = wordDefinitionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string word)
        {
            var definition = await _wordDefinitionService.GetWordDefinition(word);

            if (definition == null)
            {
                return NotFound();
            }

            return Ok(definition);
        }
    }
}
