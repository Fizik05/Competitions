using Competitions.Core.Models;

namespace Competitions.Core.Abstractions.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}