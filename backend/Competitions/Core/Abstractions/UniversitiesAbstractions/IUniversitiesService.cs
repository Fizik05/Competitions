using Competitions.Core.Models;
using CSharpFunctionalExtensions;

namespace Competitions.Core.Abstractions.UniversittiesAbstractions
{
    public interface IUniversitiesService
    {
        Task<Result<University>> CreateUniversity(University university);
        Task<Result<int>> DeleteUniversity(int id);
        Task<Result<List<University>>> GetAllUniversities();
        Task<Result<List<Team>>> GetTeamsOfUniversity(int id);
        Task<Result<University>> GetUniversityById(int id);
        Task<Result<University>> UpdateUniversity(int id, string name);
    }
}