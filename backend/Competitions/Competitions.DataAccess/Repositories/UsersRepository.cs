using Competitions.Core.Abstractions.UsersAbstractions;
using Competitions.Core.Models;
using Competitions.DataAccess.Entities;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Competitions.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly CompetitionsDbContext _context;

        public UsersRepository(CompetitionsDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            int newId = await _context.Users.MaxAsync(s => (int?)s.Id) ?? 0;
            user.Id = ++newId;

            var userEntity = new UserEntity()
            {
                Id = user.Id,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("The User with this email is not found");

            var user = User.Create(
                userEntity.Id,
                userEntity.Username,
                userEntity.PasswordHash,
                email);

            return user;
        }
    }
}
