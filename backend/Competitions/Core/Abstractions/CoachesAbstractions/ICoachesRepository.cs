using Competitions.Core.Models;

namespace Competitions.Core.Abstractions.CoachesAbstractions
{
    public interface ICoachesRepository
    {
        Task<Coach> Create(Coach coach);
        Task<int> Delete(int id);
        Task<List<Coach>> Get();
        Task<Coach?> GetById(int id);
        Task<List<Team>> GetTeams(int id);
        Task<Coach> Update(int id, string name, string surname, DateTime dateOfBirth);
    }
}