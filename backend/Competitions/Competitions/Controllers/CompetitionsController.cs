using Competitions.Application.Services;
using Competitions.Contracts.Competitions;
using Competitions.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Competitions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetitionsController : ControllerBase
    {
        private CompetitionsService _competitionsService;
        public CompetitionsController(CompetitionsService competitionsService)
        {
            _competitionsService = competitionsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompetitionsResponse>>> GetAllCompetitions()
        {
            var result = await _competitionsService.GetAllCompetitions();

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var competitions = result.Value;

            var response = competitions
                .Select(c => new CompetitionsResponse(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.KindOfSport))
                .ToList();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CompetitionsResponse>> GetCompetitionById(int id)
        {
            var result = await _competitionsService.GetCompetitionById(id);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var competition = result.Value;
            var response = new CompetitionsResponse(
                competition.Id,
                competition.Name,
                competition.Description,
                competition.KindOfSport);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CompetitionsResponse>> CreateCompetition([FromBody] CompetitionRequest request)
        {
            var competition = Competition.Create(
                0,
                request.Name,
                request.Description,
                request.KindOfsportId,
                null).competition;

            var result = await _competitionsService.CreateCompetition(competition);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            competition = result.Value;
            var response = new CompetitionsResponse(
                competition.Id,
                competition.Name,
                competition.Description,
                competition.KindOfSport);

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CompetitionsResponse>> UpdateCompetition(int id, [FromBody] CompetitionRequest request)
        {
            var result = await _competitionsService.UpdateCompetition(
                id,
                request.Name,
                request.Description,
                request.KindOfsportId);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var competition = result.Value;
            var response = new CompetitionsResponse(
                id,
                competition.Name,
                competition.Description,
                competition.KindOfSport);

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteCompetition(int id)
        {
            var result = await _competitionsService.DeleteCompetition(id);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
