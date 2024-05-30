using Competitions.Contracts.Competitions;
using Competitions.Contracts.Teams;
using Competitions.Core.Abstractions.CompetitionsAbstractions;
using Competitions.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Competitions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetitionsController : ControllerBase
    {
        private ICompetitionsService _competitionsService;
        public CompetitionsController(ICompetitionsService competitionsService)
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

        [HttpGet("{id:int}/Teams")]
        public async Task<ActionResult<List<TeamsResponse>>> GetTeamsOfCompetition(int id)
        {
            var result = await _competitionsService.GetTeamsOfCompetition(id);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var teams = result.Value;

            var response = teams
                .Select(t => new TeamsResponse(
                    t.Id,
                    t.Name,
                    t.KindOfSport,
                    t.University,
                    t.Coach))
                .ToList();

            return Ok(response);
        }

        [HttpPost("{id:int}/Teams/Add/{teamId:int}")]
        public async Task<ActionResult<List<Team>>> AddTeam(int id, int teamId)
        {
            var result = await _competitionsService.AddTeam(id, teamId);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var teams = result.Value;

            var response = teams
                .Select(t => new TeamsResponse(
                    t.Id,
                    t.Name,
                    t.KindOfSport,
                    t.University,
                    t.Coach))
                .ToList();

            return Ok(response);
        }

        [HttpDelete("{id:int}/Teams/Delete/{teamId:int}")]
        public async Task<ActionResult<List<Team>>> DeleteTeam(int id, int teamId)
        {
            var result = await _competitionsService.DeleteTeam(id, teamId);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var teams = result.Value;

            var response = teams
                .Select(t => new TeamsResponse(
                    t.Id,
                    t.Name,
                    t.KindOfSport,
                    t.University,
                    t.Coach))
                .ToList();

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
