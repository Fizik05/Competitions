using Competitions.Core.Abstractions.StudentsAbstractions;
using Competitions.Core.Models;
using Competitions.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

namespace Competitions.DataAccess.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly CompetitionsDbContext _context;

        public StudentsRepository(CompetitionsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> Get()
        {
            var studentsEntities = await _context.Students
                .Include(s => s.Team)
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

            return students;
        }

        public async Task<(Student? student, string error)> GetById(int id)
        {
            var error = string.Empty;

            var studentEntity = await _context.Students
                .Include(s => s.Team)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            if (studentEntity is null)
            {
                error = "The Student with this Id is not exists";
                return (null, error);
            }

            var teamEntity = await _context.Teams
                .Include(t => t.KindOfSport)
                .Include(t => t.University)
                .Include(t => t.Coach)
                .Where(t => t.Id == studentEntity.TeamId)
                .FirstOrDefaultAsync();

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

            var student = Student.Create(studentEntity.Id, studentEntity.Name, studentEntity.Surname, studentEntity.DateOfBirth, studentEntity.TeamId, team).student;

            return (student, error);
        }

        public async Task<(Student? student, string error)> Create(Student student)
        {
            var error = string.Empty;

            var teamEntity = await _context.Teams
                .Include(t => t.KindOfSport)
                .Include(t => t.University)
                .Include(t => t.Coach)
                .Where(t => t.Id == student.TeamId)
                .FirstOrDefaultAsync();

            if (teamEntity is null)
            {
                error = "The Team with this id is not exists";
                return (null, error);
            }

            int newId = await _context.Students.MaxAsync(s => (int?)s.Id) ?? 0;
            student.Id = ++newId;

            var studentEntity = new StudentEntity
            {
                Id = student.Id,
                Name = student.Name,
                Surname = student.Surname,
                DateOfBirth = student.DateOfBirth,
                TeamId = student.TeamId,
            };

            await _context.Students.AddAsync(studentEntity);
            await _context.SaveChangesAsync();

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

            student.Team = team;

            return (student, error);
        }

        public async Task<(Student?, string)> Update(int id, string name, string surname, DateTime dateOfBirth, int teamId)
        {
            var error = string.Empty;

            var teamEntity = await _context.Teams
                .Include(t => t.KindOfSport)
                .Include(t => t.University)
                .Include(t => t.Coach)
                .Where(t => t.Id == teamId)
                .FirstOrDefaultAsync();

            if (teamEntity is null)
            {
                error = "The Team with this id is not exists. Error during save to database";
                return (null, error);
            }

            await _context.Students
                .Where(s => s.Id == id)
                .ExecuteUpdateAsync(e => e
                    .SetProperty(s => s.Name, s => name)
                    .SetProperty(s => s.Surname, s => surname)
                    .SetProperty(s => s.DateOfBirth, s => DateTime.SpecifyKind(dateOfBirth, DateTimeKind.Utc))
                    .SetProperty(s => s.TeamId, s => teamId));

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

            var student = Student.Create(id, name, surname, dateOfBirth, teamId, team).student;

            return (student, error);
        }

        public async Task<int> Delete(int id)
        {
            await _context.Students
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
