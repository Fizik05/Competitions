using Competitions.Contracts.Competitions;
using Competitions.Contracts.Students;
using Competitions.Contracts.Teams;
using Competitions.Core.Abstractions.TeamsAbstractions;
using Competitions.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Competitions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController: ControllerBase
    {
        private ITeamsService _teamsService;
        public TeamsController(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TeamsResponse>>> GetAllTeams()
        {
            var result = await _teamsService.GetAllTeams();

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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TeamsResponse>> GetTeamById(int id)
        {
            var result = await _teamsService.GetTeamById(id);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var team = result.Value;
            var response = new TeamsResponse(
                team.Id,
                team.Name,
                team.KindOfSport,
                team.University,
                team.Coach);

            return Ok(response);
        }

        [HttpGet("{id:int}/Students")]
        public async Task<ActionResult<List<StudentsResponse>>> GetStudentsOfTeam(int id)
        {
            var result = await _teamsService.GetStudentOfTeam(id);

            var students = result.Value;

            var response = students
                .Select(s => new StudentsResponse(
                    s.Id,
                    s.Name,
                    s.Surname,
                    s.DateOfBirth,
                    s.Team))
                .ToList();

            return Ok(response);
        }

        [HttpGet("{id:int}/Competitions")]
        public async Task<ActionResult<List<CompetitionsResponse>>> GetCompetitionsOfTeam(int id)
        {
            var result = await _teamsService.GetCompetitionsOfTeam(id);

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

        [HttpPost("{id:int}/Competitions/Add/{competitionId:int}")]
        public async Task<ActionResult<List<CompetitionsResponse>>> AddCompetition(int id, int competitionId)
        {
            var result = await _teamsService.AddCompetition(id, competitionId);

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

        [HttpDelete("{id:int}/Competitions/Delete/{competitionsId:int}")]
        public async Task<ActionResult<List<CompetitionsResponse>>> DeleteCompetition(int id, int competitionId)
        {
            var result = await _teamsService.AddCompetition(id, competitionId);

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

        [HttpPost]
        public async Task<ActionResult<TeamsResponse>> CreateTeam([FromBody] TeamRequest request)
        {
            var team = Team.Create(
                0,
                request.Name,
                request.KindOfSportId,
                request.UniversityId,
                request.CoachId,
                null,
                null,
                null).team;

            var result = await _teamsService.CreateTeam(team);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            team = result.Value;
            var response = new TeamsResponse(
                team.Id,
                team.Name,
                team.KindOfSport,
                team.University,
                team.Coach);

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TeamsResponse>> UpdateTeam(
            int id,
            [FromBody] TeamRequest request)
        {
            var result = await _teamsService.UpdateTeam(
                id,
                request.Name,
                request.KindOfSportId,
                request.UniversityId,
                request.CoachId);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var team = result.Value;
            var response = new TeamsResponse(
                id,
                team.Name,
                team.KindOfSport,
                team.University,
                team.Coach);

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteTeam(int id)
        {
            var result = await _teamsService.DeleteTeam(id);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result);
        }
    }
}
