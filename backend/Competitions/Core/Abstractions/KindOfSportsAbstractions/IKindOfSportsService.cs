using Competitions.Core.Models;

namespace Competitions.Core.Abstractions.KindOfSportsAbstractions
{
    public interface IKindOfSportsService
    {
        Task<KindOfSport> CreateKindOfSport(KindOfSport kindOfSport);
        Task<int> DeleteKindOfSport(int id);
        Task<List<KindOfSport>> GetAllKindsOfSport();
        Task<KindOfSport?> GetKindOfSportById(int id);
        Task<KindOfSport> UpdateKindOfSport(int id, string name);
    }
}