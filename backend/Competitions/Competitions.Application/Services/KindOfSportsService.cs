using Competitions.Core.Abstractions.KindOfSportsAbstractions;
using Competitions.Core.Models;

namespace Competitions.Application.Services
{
    public class KindOfSportsService : IKindOfSportsService
    {
        private IKindOfSportsRepository _kindOfSportRepository;

        public KindOfSportsService(IKindOfSportsRepository kindOfSportRepository)
        {
            _kindOfSportRepository = kindOfSportRepository;
        }

        public async Task<List<KindOfSport>> GetAllKindsOfSport()
        {
            return await _kindOfSportRepository.Get();
        }

        public async Task<KindOfSport?> GetKindOfSportById(int id)
        {
            var data = await _kindOfSportRepository.GetById(id);

            return data;
        }

        public async Task<KindOfSport> CreateKindOfSport(KindOfSport kindOfSport)
        {
            return await _kindOfSportRepository.Create(kindOfSport);
        }

        public async Task<KindOfSport> UpdateKindOfSport(int id, string name)
        {
            return await _kindOfSportRepository.Update(id, name);
        }

        public async Task<int> DeleteKindOfSport(int id)
        {
            return await _kindOfSportRepository.Delete(id);
        }
    }
}
