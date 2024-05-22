using Competitions.Core.Models;
using Competitions.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

namespace Competitions.DataAccess.Repositories
{
    public class StudentsRepository
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

            var students = studentsEntities
                .Select(s => Student.Create(s.Id, s.Name, s.Surname, s.DateOfBirth, s.TeamId).student)
                .ToList();

            return students;
        }

        public async Task<(Student? student, string error)> GetById(int id)
        {
            var error = string.Empty;

            var studentEntity = await _context.Students
                .FindAsync(id);

            if (studentEntity is null)
            {
                error = "The Student with this Id is not exists";
                return (null, error);
            }

            var student = Student.Create(studentEntity.Id, studentEntity.Name, studentEntity.Surname, studentEntity.DateOfBirth, studentEntity.TeamId).student;

            return (student, error);
        }

        public async Task<(Student? student, string error)> Create(Student student)
        {
            var error = string.Empty;

            var teamExists = await _context.Teams.AnyAsync(t => t.Id == student.TeamId);

            if (!teamExists)
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

            return (student, error);
        }

        public async Task<(Student?, string)> Update(int id, string name, string surname, DateTime dateOfBirth, int teamId)
        {
            var error = string.Empty;

            var teamExists = await _context.Teams.AnyAsync(t => t.Id == teamId);

            if (!teamExists)
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

            var student = Student.Create(id, name, surname, dateOfBirth, teamId).student;

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
