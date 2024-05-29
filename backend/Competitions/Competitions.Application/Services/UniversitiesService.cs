using Competitions.Core.Models;
using Competitions.DataAccess.Repositories;
using CSharpFunctionalExtensions;

namespace Competitions.Application.Services
{
    public class UniversitiesService
    {
        private UniversitiesRepository _universitiesRepository;
        public UniversitiesService(UniversitiesRepository repository)
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
