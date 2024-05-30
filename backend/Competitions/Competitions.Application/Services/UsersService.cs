using Competitions.Core.Abstractions.Auth;
using Competitions.Core.Abstractions.UsersAbstractions;
using Competitions.Core.Models;

namespace Competitions.Application.Services
{
    public class UsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvidet;

        public UsersService(IUsersRepository usersRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtProvidet = jwtProvider;
        }

        public async Task Register(string username, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(
                0,
                username,
                hashedPassword,
                email);

            await _usersRepository.Add(user);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvidet.GenerateToken(user);

            return token;
        }
    }
}
