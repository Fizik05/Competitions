using Competitions.Core.Abstractions.CompetitionsAbstractions;
using Competitions.Core.Models;
using Competitions.DataAccess.Entities;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Competitions.DataAccess.Repositories
{
    public class CompetitionsRepository : ICompetitionsRepository
    {
        private readonly CompetitionsDbContext _context;

        public CompetitionsRepository(CompetitionsDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Competition>>> Get()
        {
            var competitionsEntities = await _context.Competitions
                .Include(c => c.KindOfSport)
                .OrderBy(c => c.Id)
                .ToListAsync();

            var competitions = competitionsEntities
                .Select(c => Competition.Create(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.KindOfSportId,
                    KindOfSport.Create(c.KindOfSport.Id, c.KindOfSport.Name).kindOfSport).competition)
                .ToList();

            return Result.Success(competitions);
        }

        public async Task<Result<Competition>> GetById(int id)
        {
            var competitionEntity = await _context.Competitions
                .Include(c => c.KindOfSport)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (competitionEntity is null)
            {
                return Result.Failure<Competition>("The Competition with this id is not found");
            }

            var kindOfSport = KindOfSport.Create(
                competitionEntity.KindOfSport.Id,
                competitionEntity.KindOfSport.Name);

            var competition = Competition.Create(
                competitionEntity.Id,
                competitionEntity.Name,
                competitionEntity.Description,
                competitionEntity.KindOfSportId,
                kindOfSport.kindOfSport).competition;

            return Result.Success(competition);
        }

        public async Task<Result<List<Team>>> GetTeams(int id)
        {
            var competitionEntity = await _context.Competitions
                .Include(c => c.KindOfSport)
                .Include(c => c.Teams)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            var teamsEntity = await _context.Teams
                .Include(t => t.KindOfSport)
                .Include(t => t.University)
                .Include(t => t.Coach)
                .Include(t => t.Competitions)
                .Where(t => t.Competitions.Contains(competitionEntity))
                .ToListAsync();

            if (competitionEntity == null)
            {
                return Result.Failure<List<Team>>("The Competition with this Id is not found");
            }

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

            return Result.Success(teams);
        }

        public async Task<Result<List<Team>>> AddTeam(int id, int teamId)
        {
            var team = await _context.Teams
                .Include(t => t.KindOfSport)
                .Include(t => t.University)
                .Include(t => t.Coach)
                .Include(t => t.Competitions)
                .Where(t => t.Id == teamId)
                .FirstOrDefaultAsync();

            if (team is null)
            {
                return Result.Failure<List<Team>>("The Team with this Id is not found");
            }

            var competition = await _context.Competitions
                .Include(c => c.Teams)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            competition.Teams.Add(team);
            await _context.SaveChangesAsync();

            return await GetTeams(id);
        }

        public async Task<Result<List<Team>>> DeleteTeam(int id, int teamId)
        {
            var team = await _context.Teams
                .Include(t => t.KindOfSport)
                .Include(t => t.University)
                .Include(t => t.Coach)
                .Include(t => t.Competitions)
                .Where(t => t.Id == teamId)
                .FirstOrDefaultAsync();

            if (team is null)
            {
                return Result.Failure<List<Team>>("The Team with this Id is not found");
            }

            var competition = await _context.Competitions
                .Include(c => c.KindOfSport)
                .Include(c => c.Teams)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            competition.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return await GetTeams(id);
        }

        public async Task<Result<Competition>> Create(Competition competition)
        {
            int newId = await _context.Competitions.MaxAsync(c => (int?)c.Id) ?? 0;
            newId++;

            var kindOfSport = await _context.KindOfSports
                .Where(k => k.Id == competition.KindOfSportId)
                .FirstOrDefaultAsync();

            if (kindOfSport is null)
            {
                return Result.Failure<Competition>("The Kind Of Sport with this id is not found");
            }

            var competitionEntity = new CompetitionEntity
            {
                Id = newId,
                Name = competition.Name,
                Description = competition.Description,
                KindOfSportId = competition.KindOfSportId
            };

            await _context.Competitions.AddAsync(competitionEntity);
            await _context.SaveChangesAsync();

            competition.Id = newId;
            competition.KindOfSport = KindOfSport.Create(
                competitionEntity.KindOfSport.Id,
                competitionEntity.KindOfSport.Name).kindOfSport;

            return Result.Success(competition);
        }

        public async Task<Result<Competition>> Update(int id, string name, string description, int kindOfSportId)
        {
            var kindOfSportEntity = await _context.KindOfSports
                .Where(k => k.Id == kindOfSportId)
                .FirstOrDefaultAsync();

            if (kindOfSportEntity is null)
            {
                return Result.Failure<Competition>("The Kind Of Sport with this id is not found");
            }

            await _context.Competitions
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(a => a
                    .SetProperty(c => c.Name, c => name)
                    .SetProperty(c => c.Description, c => description)
                    .SetProperty(c => c.KindOfSportId, c => kindOfSportId));

            var kindOfSport = KindOfSport.Create(
                kindOfSportId,
                kindOfSportEntity.Name).kindOfSport;


            var competition = Competition.Create(
                id,
                name,
                description,
                kindOfSportId,
                kindOfSport).competition;

            return Result.Success(competition);
        }

        public async Task<Result<int>> Delete(int id)
        {
            await _context.Competitions
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();

            return Result.Success(id);
        }
    }
}
