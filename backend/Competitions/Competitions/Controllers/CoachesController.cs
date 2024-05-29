using Competitions.Application.Services;
using Competitions.Contracts.Coaches;
using Competitions.Contracts.Teams;
using Competitions.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Competitions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachesController: ControllerBase
    {
        private readonly CoachesService _coachesService;

        public CoachesController(CoachesService coachesService)
        {
            _coachesService = coachesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CoachesResponse>>> GetCoaches()
        {
            var coaches = await _coachesService.GetAllCoaches();

            var response = coaches
                .Select(c => new CoachesResponse(c.Id, c.Name, c.Surname, c.DateOfBirth))
                .ToList();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CoachesResponse>> GetCoachById(int id)
        {
            var coach = await _coachesService.GetCoachById(id);

            if (coach is null)
            {
                return BadRequest($"The Coach with id {id} is not found");
            }

            var response = new CoachesResponse(coach.Id, coach.Name, coach.Surname, coach.DateOfBirth);

            return Ok(response);
        }

        [HttpGet("{id:int}/Teams")]
        public async Task<ActionResult<List<TeamsResponse>>> GetTeamsOfCoach(int id)
        {
            var teams = await _coachesService.GetAllTeamsOfCoach(id);

            var response = teams
                .Select(t => new TeamsResponse(t.Id, t.Name, t.KindOfSport, t.University, t.Coach))
                .ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CoachesResponse>> CreateCoach([FromBody] CoachRequest request)
        {
            var (coach, error) = Coach.Create(
                0,
                request.Name,
                request.Surname,
                request.DateOfBirth);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var createdCoach= await _coachesService.CreateCoach(coach);
            var responseCoach= new CoachesResponse(createdCoach.Id, createdCoach.Name, createdCoach.Surname, createdCoach.DateOfBirth);

            return Ok(responseCoach);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CoachesResponse>> UpdateCoach(int id, [FromBody] CoachRequest request)
        {
            var coach = await _coachesService.UpdateCoach(id, request.Name, request.Surname, request.DateOfBirth);

            var responseCoach= new CoachesResponse(coach.Id, coach.Name, coach.Surname, coach.DateOfBirth);

            return Ok(responseCoach);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteCoach(int id)
        {
            var coachId = await _coachesService.DeleteCoach(id);

            return coachId;
        }
    }
}
