using Competitions.Core.Abstractions.UniversittiesAbstractions;
using Competitions.Core.Models;
using CSharpFunctionalExtensions;

namespace Competitions.Application.Services
{
    public class UniversitiesService : IUniversitiesService
    {
        private IUniversitiesRepository _universitiesRepository;
        public UniversitiesService(IUniversitiesRepository repository)
        {
            _universitiesRepository = repository;
        }

        public async Task<Result<List<University>>> GetAllUniversities()
        {
            return await _universitiesRepository.Get();
        }

        public async Task<Result<University>> GetUniversityById(int id)
        {
            return await _universitiesRepository.GetById(id);
        }

        public async Task<Result<List<Team>>> GetTeamsOfUniversity(int id)
        {
            return await _universitiesRepository.GetTeams(id);
        }

        public async Task<Result<University>> CreateUniversity(University university)
        {
            return await _universitiesRepository.Create(university);
        }

        public async Task<Result<University>> UpdateUniversity(int id, string name)
        {
            return await _universitiesRepository.Update(id, name);
        }

        public async Task<Result<int>> DeleteUniversity(int id)
        {
            return await _universitiesRepository.Delete(id);
        }
    }
}
