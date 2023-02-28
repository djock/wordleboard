using Microsoft.AspNetCore.Mvc;
using wordleboard.Db;
using wordleboard.Models;
using wordleboard.Services;

namespace wordleboard.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitResultController : ControllerBase
    {
        private readonly IWordleRepository _wordleRepo;
        private readonly UserService _userService;
        private readonly IWordleService _wordleService;
        //private readonly IWordleResultsRepository _wordleResultsRepository;
        //private readonly IDictionaryRepository _dictionaryRepository;

        public SubmitResultController(IWordleRepository wordleRepo, UserService userService, IWordleService wordleService)//, IWordleResultsRepository wordleResultsRepository, IDictionaryRepository dictionaryRepository)
        {
            _wordleRepo = wordleRepo;
            _userService = userService;
            _wordleService = wordleService;
            //_wordleResultsRepository = wordleResultsRepository;
            //_dictionaryRepository = dictionaryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] WordleScore wordleScore)
        {
            try
            {
                var user = _userService.ActiveUser;

                UserWordle userWordle = new UserWordle
                {
                    WordleId = DateTime.Now.Subtract(new DateTime(2021, 6, 19)).Days,
                    UserId = user.Id,
                    Points = wordleScore.Points,
                    Bonus = wordleScore.Bonus
                };

                if (_wordleRepo.AllWordlesForUser(user.Id).Exists(x => x.WordleId == userWordle.WordleId && x.UserId == userWordle.UserId))
                {
                    _wordleRepo.UpdateWordle(userWordle);
                }
                else
                {
                    _wordleRepo.AddWordle(userWordle);
                }

                //var today = await _wordleResultsRepository.GetToday();

                //WordDefinition definition = null;

                //if (!string.IsNullOrEmpty(today))
                //{
                //    HttpContext.Response.Cookies.Append($"wordle_{AppUtils.TodayWordleId}", today);

                //    definition = await _dictionaryRepository.GetWordDefinition(today);

                //    var response = new
                //    {
                //        message = "Success!",
                //        data = definition,
                //    };

                //    // Serialize the object to JSON
                //    var json = JsonSerializer.Serialize(response);

                //    // Return the JSON response
                //    return new ContentResult
                //    {
                //        Content = json,
                //        ContentType = "application/json",
                //        StatusCode = 200,
                //    };
                //}

                //return Ok(today);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
