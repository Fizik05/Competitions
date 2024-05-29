using Competitions.Core.Models;
using Competitions.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Competitions.DataAccess.Repositories
{
    public class CoachesRepository
    {
        private readonly CompetitionsDbContext _context;

        public CoachesRepository(CompetitionsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Coach>> Get()
        {
            var coachesEntity = await _context.Coaches
                .AsNoTracking()
                .ToListAsync();

            var coaches = coachesEntity
                .Select(c => Coach.Create(c.Id, c.Name, c.Surname, c.DateOfBirth).coach)
                .ToList();

            return coaches;
        }

        public async Task<Coach?> GetById(int id)
        {
            var coachEntity = await _context.Coaches
                .FindAsync(id);

            if (coachEntity is null) { return null; }

            var coach = Coach.Create(coachEntity.Id, coachEntity.Name, coachEntity.Surname, coachEntity.DateOfBirth).coach;

            return coach;
        }

        public async Task<List<Team>> GetTeams(int id)
        {
            var teamsEntity = await _context.Teams
                .Include(t => t.KindOfSport)
                .Include(t => t.University)
                .Include(t => t.Coach)
                .Where(t => t.CoachId == id)
                .ToListAsync();

            var teams = teamsEntity
                .Select(t => Team.Create(
                    t.Id,
                    t.Name,
                    t.KindOfSportId,
                    t.UniversityId,
                    t.CoachId,
                    KindOfSport.Create(t.KindOfSportId, t.KindOfSport.Name).kindOfSport,
                    University.Create(t.UniversityId, t.University.Name).university,
                    Coach.Create(t.CoachId, t.Coach.Name, t.Coach.Surname, t.Coach.DateOfBirth).coach).team)
                .ToList();

            return teams;
        }

        public async Task<Coach> Create(Coach coach)
        {
            int newId = await _context.Coaches.MaxAsync(s => (int?)s.Id) ?? 0;
            coach.Id = ++newId;

            var coachEntity = new CoachEntity
            {
                Id = coach.Id,
                Name = coach.Name,
                Surname = coach.Surname,
                DateOfBirth = coach.DateOfBirth
            };

            await _context.Coaches.AddAsync(coachEntity);
            await _context.SaveChangesAsync();

            return coach;
        }

        public async Task<Coach> Update(int id, string name, string surname, DateTime dateOfBirth)
        {
            await _context.Coaches
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.Name, c => name)
                    .SetProperty(c => c.Surname, c => surname)
                    .SetProperty(c => c.DateOfBirth, c => DateTime.SpecifyKind(dateOfBirth, DateTimeKind.Utc)));

            var coach = Coach.Create(id, name, surname, dateOfBirth).coach;

            return coach;
        }

        public async Task<int> Delete(int id)
        {
            await _context.Coaches
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
