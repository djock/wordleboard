using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using wordleboard.Models;
using wordleboard.Utils;

namespace wordleboard.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitWordleController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWordleRepository _wordleRepo;
        private readonly IWordleResultsRepository _wordleResultsRepository;
        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly ILogger<HomeController> _logger;

        public SubmitWordleController(IWordleRepository wordleRepo, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IWordleResultsRepository wordleResultsRepository, IDictionaryRepository dictionaryRepository)
        {
            _wordleRepo = wordleRepo;
            _logger = logger;
            _userManager = userManager;
            _wordleResultsRepository = wordleResultsRepository;
            _dictionaryRepository = dictionaryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] WordleScore wordleScore)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;

                UserWordle userWordle = new UserWordle
                {
                    WordleId = AppUtils.TodayWordleId,
                    UserId = user.Id,
                    Points = wordleScore.Points,
                    Bonus = wordleScore.Bonus
                };

                // if (_wordleRepo.AllWordlesForUser(user.Id).Exists(x => x.WordleId == userWordle.WordleId && x.UserId == userWordle.UserId))
                // {
                //     _wordleRepo.UpdateWordle(userWordle);
                // }
                // else
                // {
                //     _wordleRepo.AddWordle(userWordle);
                // }

                var today = await _wordleResultsRepository.GetToday();
                
                WordDefinition definition = null;
                
                if (!string.IsNullOrEmpty(today))
                {
                    HttpContext.Response.Cookies.Append($"wordle_{AppUtils.TodayWordleId}", today);

                    definition = await _dictionaryRepository.GetWordDefinition(today);
                    
                    Console.WriteLine("definition " + definition.Word);
                    
                    return Ok(definition.Word);
                }

                return Ok(today);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
