using Competitions.Core.Models;
using Competitions.DataAccess.Repositories;
using CSharpFunctionalExtensions;

namespace Competitions.Application.Services
{
    public class CompetitionsService
    {
        private CompetitionsRepository _competitionsRepository;
        public CompetitionsService(CompetitionsRepository competitionsRepository)
        {
            _competitionsRepository = competitionsRepository;
        }

        public async Task<Result<List<Competition>>> GetAllCompetitions()
        {
            return await _competitionsRepository.Get();
        }

        public async Task<Result<Competition>> GetCompetitionById(int id)
        {
            return await _competitionsRepository.GetById(id);
        }

        public async Task<Result<List<Team>>> GetTeamsOfCompetition(int id)
        {
            return await _competitionsRepository.GetTeams(id);
        }

        public async Task<Result<List<Team>>> AddTeam(int id, int teamId)
        {
            return await _competitionsRepository.AddTeam(id, teamId);
        }

        public async Task<Result<List<Team>>> DeleteTeam(int id, int teamId)
        {
            return await _competitionsRepository.DeleteTeam(id, teamId);
        }

        public async Task<Result<Competition>> CreateCompetition(Competition competition)
        {
            return await _competitionsRepository.Create(competition);
        }

        public async Task<Result<Competition>> UpdateCompetition(int id, string name, string description, int kindOfSportId)
        {
            return await _competitionsRepository.Update(
                id,
                name,
                description,
                kindOfSportId);
        }

        public async Task<Result<int>> DeleteCompetition(int id)
        {
            return await _competitionsRepository.Delete(id);
        }
    }
}
