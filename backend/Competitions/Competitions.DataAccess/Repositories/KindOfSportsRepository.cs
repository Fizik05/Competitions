using Competitions.Core.Abstractions.KindOfSportsAbstractions;
using Competitions.Core.Models;
using Competitions.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Competitions.DataAccess.Repositories
{
    public class KindOfSportsRepository : IKindOfSportsRepository
    {
        private readonly CompetitionsDbContext _context;

        public KindOfSportsRepository(CompetitionsDbContext context)
        {
            _context = context;
        }

        public async Task<List<KindOfSport>> Get()
        {
            var kindOfSportsEntites = await _context.KindOfSports
                .AsNoTracking()
                .OrderBy(k => k.Id)
                .ToListAsync();

            var kindOfSports = kindOfSportsEntites
                .Select(k => KindOfSport.Create(k.Id, k.Name).kindOfSport)
                .ToList();

            return kindOfSports;
        }

        public async Task<KindOfSport?> GetById(int id)
        {
            var kindOfSportEntity = await _context.KindOfSports
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.Id == id);

            if (kindOfSportEntity == null)
            {
                return null;
            }

            var kindOfSport = KindOfSport.Create(kindOfSportEntity.Id, kindOfSportEntity.Name).kindOfSport;

            return kindOfSport;
        }

        public async Task<KindOfSport> Create(KindOfSport kindOfSport)
        {
            int newId = await _context.KindOfSports.MaxAsync(s => (int?)s.Id) ?? 0;
            newId++;

            var kindOfSportEntity = new KindOfSportEntity
            {
                Id = newId,
                Name = kindOfSport.Name
            };

            kindOfSport.Id = newId;

            await _context.KindOfSports.AddAsync(kindOfSportEntity);
            await _context.SaveChangesAsync();

            return kindOfSport;
        }

        public async Task<KindOfSport> Update(int id, string name)
        {
            await _context.KindOfSports
                .Where(k => k.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(k => k.Name, name));

            var kindOfSport = KindOfSport.Create(id, name).kindOfSport;

            return kindOfSport;
        }

        public async Task<int> Delete(int id)
        {
            await _context.KindOfSports
                .Where(k => k.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
