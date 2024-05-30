using Competitions.Core.Abstractions.CoachesAbstractions;
using Competitions.Core.Models;

namespace Competitions.Application.Services
{
    public class CoachesService : ICoachesService
    {
        private ICoachesRepository _coachesRepository;

        public CoachesService(ICoachesRepository coachesRepository)
        {
            _coachesRepository = coachesRepository;
        }

        public async Task<List<Coach>> GetAllCoaches()
        {
            return await _coachesRepository.Get();
        }

        public async Task<Coach?> GetCoachById(int id)
        {
            return await _coachesRepository.GetById(id);
        }

        public async Task<List<Team>> GetAllTeamsOfCoach(int id)
        {
            return await _coachesRepository.GetTeams(id);
        }

        public async Task<Coach> CreateCoach(Coach coach)
        {
            return await _coachesRepository.Create(coach);
        }

        public async Task<Coach> UpdateCoach(int id, string name, string surname, DateTime dateOfBirth)
        {
            return await _coachesRepository.Update(id, name, surname, dateOfBirth);
        }

        public async Task<int> DeleteCoach(int id)
        {
            return await _coachesRepository.Delete(id);
        }
    }
}
