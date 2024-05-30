using Competitions.Core.Models;

namespace Competitions.Core.Abstractions.CoachesAbstractions
{
    public interface ICoachesService
    {
        Task<Coach> CreateCoach(Coach coach);
        Task<int> DeleteCoach(int id);
        Task<List<Coach>> GetAllCoaches();
        Task<List<Team>> GetAllTeamsOfCoach(int id);
        Task<Coach?> GetCoachById(int id);
        Task<Coach> UpdateCoach(int id, string name, string surname, DateTime dateOfBirth);
    }
}