using Competitions.Core.Models;

namespace Competitions.Core.Abstractions.KindOfSportsAbstractions
{
    public interface IKindOfSportsRepository
    {
        Task<KindOfSport> Create(KindOfSport kindOfSport);
        Task<int> Delete(int id);
        Task<List<KindOfSport>> Get();
        Task<KindOfSport?> GetById(int id);
        Task<KindOfSport> Update(int id, string name);
    }
}