using Competitions.Application.Services;
using Competitions.Contracts.KindOfSports;
using Competitions.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

namespace Competitions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KindOfSportsController: ControllerBase
    {
        private KindOfSportsService _kindOfSportsService;

        public KindOfSportsController(KindOfSportsService kindOfSportsService)
        {
            _kindOfSportsService = kindOfSportsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<KindOfSportsResponse>>> GetKindsOfSport()
        {
            var kindOfSports = await _kindOfSportsService.GetAllKindsOfSport();

            var response = kindOfSports.Select(k => new KindOfSportsResponse(k.Id, k.Name)).OrderBy(k => k.Id);

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<KindOfSportsResponse>> GetKindsOfSportById(int id)
        {
            var kindOfSport = await _kindOfSportsService.GetKindOfSportById(id);

            if (kindOfSport == null)
            {
                return BadRequest($"The kind of sport with id {id} is not found");
            }

            var response = new KindOfSportsResponse(kindOfSport.Id, kindOfSport.Name);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<KindOfSportsResponse>> CreateKindOfSport([FromBody] KindOfSportRequest request)
        {
            var (kindOfSport, error) = KindOfSport.Create(
                0,
                request.Name);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var createdKindOfSport = await _kindOfSportsService.CreateKindOfSport(kindOfSport);
            var responseKindOfSport = new KindOfSportsResponse(createdKindOfSport.Id, createdKindOfSport.Name);

            return Ok(responseKindOfSport);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<KindOfSportsResponse>> UpdateKindOfSport(int id, [FromBody] KindOfSportRequest request)
        {
            var kindOfSport = await _kindOfSportsService.UpdateKindOfSport(id, request.Name);

            var responseKindOfSport = new KindOfSportsResponse(kindOfSport.Id, kindOfSport.Name);

            return Ok(responseKindOfSport);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteKindOfSport(int id)
        {
            var kindOfSportId = await _kindOfSportsService.DeleteKindOfSport(id);

            return Ok(kindOfSportId);
        }
    }
}
