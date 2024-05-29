using Competitions.Application.Services;
using Competitions.Contracts.Universities;
using Competitions.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Competitions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversitiesController : ControllerBase
    {
        private UniversitiesService _universitiesService;
        public UniversitiesController(UniversitiesService service)
        {
            _universitiesService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<UniversitiesResponse>>> GetUniversities()
        {
            var universitiesResult = await _universitiesService.GetAllUniversities();

            if (universitiesResult.IsFailure)
            {
                return BadRequest(universitiesResult.Error);
            }

            var universities = universitiesResult.Value;

            var response = universities.Select(
                u => new UniversitiesResponse(u.Id, u.Name))
                .ToList();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UniversitiesResponse>> GetUniversityById(int id)
        {
            var universityResult = await _universitiesService.GetUniversityById(id);

            if (universityResult.IsFailure)
            {
                return BadRequest(universityResult.Error);
            }

            var university = universityResult.Value;
            var response = new UniversitiesResponse(university.Id, university.Name);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<UniversitiesResponse>> CreateUniversity([FromBody] UniversityRequest request)
        {
            var university = University.Create(0, request.Name).university;

            var universityResult = await _universitiesService.CreateUniversity(university);

            if (universityResult.IsFailure)
            {
                return BadRequest(universityResult.Error);
            }

            university = universityResult.Value;
            var response = new UniversitiesResponse(university.Id, university.Name);

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UniversitiesResponse>> UpdateUniversity(int id, [FromBody] UniversityRequest request)
        {
            var universityResult = await _universitiesService.UpdateUniversity(id, request.Name);

            if (universityResult.IsFailure)
            {
                return BadRequest(universityResult.Error);
            }

            var university = universityResult.Value;
            var response = new UniversitiesResponse(university.Id, university.Name);

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteUniversity(int id)
        {
            var universityResult = await _universitiesService.DeleteUniversity(id);

            if (universityResult.IsFailure)
            {
                return BadRequest(universityResult.Error);
            }

            return Ok(universityResult.Value);
        }
    }
}
