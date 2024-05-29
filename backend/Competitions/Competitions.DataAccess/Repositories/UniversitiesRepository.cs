using Competitions.Core.Models;
using Competitions.DataAccess.Entities;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Competitions.DataAccess.Repositories
{
    public class UniversitiesRepository
    {
        private readonly CompetitionsDbContext _context;

        public UniversitiesRepository(CompetitionsDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<University>>> Get()
        {
            var universitiesEntities = await _context.Universities
                .AsNoTracking()
                .OrderBy(u => u.Id)
                .ToListAsync();

            var universities = universitiesEntities
                .Select(u => University.Create(u.Id, u.Name).university)
                .ToList();

            return Result.Success(universities);
        }

        public async Task<Result<University>> GetById(int id)
        {
            var universityEntity = await _context.Universities
                .FindAsync(id);

            if (universityEntity is null) 
            { 
                return Result.Failure<University>("The University with this Id is not found"); 
            }

            var university = University.Create(universityEntity.Id, universityEntity.Name).university;

            return Result.Success(university);
        }

        public async Task<Result<University>> Create(University university)
        {
            int newId = await _context.Universities.MaxAsync(u => (int?)u.Id) ?? 0;
            newId++;

            var universityEntity = new UniversityEntity
            {
                Id = newId,
                Name = university.Name
            };

            university.Id = newId;

            await _context.Universities.AddAsync(universityEntity);
            await _context.SaveChangesAsync();

            return Result.Success(university);
        }

        public async Task<Result<University>> Update(int id, string name)
        {
            await _context.Universities
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.Name, u => name));

            var university = University.Create(id, name).university;

            return Result.Success(university);
        }

        public async Task<Result<int>> Delete(int id)
        {
            await _context.Universities
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();

            return Result.Success(id);
        }
    }
}
