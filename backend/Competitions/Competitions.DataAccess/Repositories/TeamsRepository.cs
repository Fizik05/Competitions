using Competitions.Core.Models;
using Competitions.DataAccess.Entities;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Competitions.DataAccess.Repositories
{
    public class TeamsRepository
    {
        private readonly CompetitionsDbContext _context;

        public TeamsRepository(CompetitionsDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Team>>> Get()
        {
            var teamsEntities = await _context.Teams
                .Include(t => t.KindOfSport)
                .Include(t => t.University)
                .Include(t => t.Coach)
                .OrderBy(t => t.Id)
                .ToListAsync();

            var teams = teamsEntities
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

        public async Task<Result<Team>> GetById(int id)
        {
            var teamEntity = await _context.Teams
                .Include(t => t.KindOfSport)
                .Include(t => t.University)
                .Include(t => t.Coach)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (teamEntity is null)
            {
                return Result.Failure<Team>("The team with this id is not found");
            }

            var kindOfSport = KindOfSport.Create(
                teamEntity.KindOfSportId,
                teamEntity.KindOfSport.Name).kindOfSport;
            var university = University.Create(
                teamEntity.UniversityId,
                teamEntity.University.Name).university;
            var coach = Coach.Create(
                teamEntity.CoachId,
                teamEntity.Coach.Name,
                teamEntity.Coach.Surname,
                teamEntity.Coach.DateOfBirth).coach;

            var team = Team.Create(
                teamEntity.Id,
                teamEntity.Name,
                teamEntity.KindOfSportId,
                teamEntity.UniversityId,
                teamEntity.CoachId,
                kindOfSport,
                university,
                coach).team;

            return Result.Success(team);
        }

        public async Task<Result<List<Student>>> GetStudents(int id)
        {
            var studentsEntities = await _context.Students
                .Include(s => s.Team)
                .Where(s => s.TeamId == id)
                .ToListAsync();

            var teams = await _context.Teams
                .Include(t => t.KindOfSport)
                .Include(t => t.University)
                .Include(t => t.Coach)
                .ToListAsync();

            var students = studentsEntities
                .Select(s => Student.Create(
                    s.Id,
                    s.Name,
                    s.Surname,
                    s.DateOfBirth,
                    s.TeamId,
                    Team.Create(
                        s.TeamId,
                        s.Team.Name,
                        s.Team.KindOfSportId,
                        s.Team.UniversityId,
                        s.Team.CoachId,
                        KindOfSport.Create(s.Team.KindOfSport.Id, s.Team.KindOfSport.Name).kindOfSport,
                        University.Create(s.Team.University.Id, s.Team.University.Name).university,
                        Coach.Create(s.Team.Coach.Id, s.Team.Coach.Name, s.Team.Coach.Surname, s.Team.Coach.DateOfBirth).coach).team)
                    .student)
                .ToList();

            return Result.Success(students);
        }

        public async Task<Result<Team>> Create(Team team)
        {
            int newId = await _context.Teams.MaxAsync(c => (int?)c.Id) ?? 0;
            newId++;

            var kindOfSport = await _context.KindOfSports
                .Where(k => k.Id == team.KindOfSportId)
                .FirstOrDefaultAsync();
            var university = await _context.Universities
                .Where(u => u.Id == team.UniversityId)
                .FirstOrDefaultAsync();
            var coach = await _context.Coaches
                .Where(c => c.Id == team.CoachId)
                .FirstOrDefaultAsync();

            if (kindOfSport is null || university is null || coach is null)
            {
                return Result.Failure<Team>("The additional field was not found, but which..\nI'm too lazy to write all code");
            }

            var teamEntity = new TeamEntity
            {
                Id = team.Id,
                Name = team.Name,
                KindOfSportId = team.KindOfSportId,
                UniversityId = team.UniversityId,
                CoachId = team.CoachId
            };

            await _context.AddAsync(teamEntity);
            await _context.SaveChangesAsync();

            team.Id = newId;
            team.KindOfSport = KindOfSport.Create(
                team.KindOfSportId,
                kindOfSport.Name).kindOfSport;
            team.University = University.Create(
                team.UniversityId,
                university.Name).university;
            team.Coach = Coach.Create(
                coach.Id,
                coach.Name,
                coach.Surname,
                coach.DateOfBirth).coach;

            return Result.Success(team);
        }

        public async Task<Result<Team>> Update(
            int id,
            string name,
            int kindOfSportId,
            int universityId,
            int coachId)
        {
            var kindOfSportEntity = await _context.KindOfSports
                .Where(k => k.Id == kindOfSportId)
                .FirstOrDefaultAsync();
            var universityEntity = await _context.Universities
                .Where(u => u.Id == universityId)
                .FirstOrDefaultAsync();
            var coachEntity = await _context.Coaches
                .Where(c => c.Id == coachId)
                .FirstOrDefaultAsync();

            if (kindOfSportEntity is null || universityEntity is null || coachEntity is null)
            {
                return Result.Failure<Team>("The additional field was not found, but which..\nI'm too lazy to write all code");
            }

            await _context.Teams
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(a => a
                    .SetProperty(t => t.Id, t => id)
                    .SetProperty(t => t.Name, t => name)
                    .SetProperty(t => t.KindOfSportId, t => kindOfSportId)
                    .SetProperty(t => t.UniversityId, t => universityId)
                    .SetProperty(t => t.CoachId, t => coachId));

            var kindOfSport = KindOfSport.Create(
                kindOfSportEntity.Id,
                kindOfSportEntity.Name).kindOfSport;
            var university = University.Create(
                universityEntity.Id,
                universityEntity.Name).university;
            var coach = Coach.Create(
                coachEntity.Id,
                coachEntity.Name,
                coachEntity.Surname,
                coachEntity.DateOfBirth).coach;

            var team = Team.Create(
                id, name, kindOfSportId, universityId, coachId,
                kindOfSport,
                university,
                coach).team;

            return Result.Success(team);
        }

        public async Task<Result<int>> Delete(int id)
        {
            await _context.Teams
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();

            return Result.Success(id);
        }
    }
}
