using Competitions.Core.Models;
using Competitions.DataAccess.Repositories;
using CSharpFunctionalExtensions;

namespace Competitions.Application.Services
{
    public class TeamsService
    {
        private TeamsRepository _teamsRepository;
        public TeamsService(TeamsRepository teamsRepository)
        {
            _teamsRepository = teamsRepository;
        }

        public async Task<Result<List<Team>>> GetAllTeams()
        {
            return await _teamsRepository.Get();
        }

        public async Task<Result<Team>> GetTeamById(int id)
        {
            return await _teamsRepository.GetById(id);
        }

        public async Task<Result<List<Student>>> GetStudentOfTeam(int id)
        {
            return await _teamsRepository.GetStudents(id);
        }

        public async Task<Result<Team>> CreateTeam(Team team)
        {
            return await _teamsRepository.Create(team);
        }

        public async Task<Result<Team>> UpdateTeam(
            int id,
            string name,
            int kindOfSportId,
            int universityId,
            int coachId)
        {
            return await _teamsRepository.Update(
                id,
                name,
                kindOfSportId,
                universityId,
                coachId);
        }

        public async Task<Result<int>> DeleteTeam(int id)
        {
            return await _teamsRepository.Delete(id);
        }
    }
}
