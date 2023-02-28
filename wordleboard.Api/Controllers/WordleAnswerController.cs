using Microsoft.AspNetCore.Mvc;
using wordleboard.Services;

namespace wordleboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordleAnswerController : ControllerBase
    {
        private readonly IWordleAnswersService _wordleAnswersService;

        public WordleAnswerController(IWordleAnswersService wordleAnswersService)
        {
            _wordleAnswersService = wordleAnswersService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return await _wordleAnswersService.GetToday();
        }
    }
}
