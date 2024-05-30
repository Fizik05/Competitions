using Competitions.Core.Models;
using CSharpFunctionalExtensions;

namespace Competitions.Core.Abstractions.UniversittiesAbstractions
{
    public interface IUniversitiesRepository
    {
        Task<Result<University>> Create(University university);
        Task<Result<int>> Delete(int id);
        Task<Result<List<University>>> Get();
        Task<Result<University>> GetById(int id);
        Task<Result<List<Team>>> GetTeams(int id);
        Task<Result<University>> Update(int id, string name);
    }
}