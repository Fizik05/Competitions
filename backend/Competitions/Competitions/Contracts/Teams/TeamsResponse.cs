using Competitions.Core.Models;

namespace Competitions.Contracts.Teams
{
    public record TeamsResponse(
        int Id,
        string Name,
        KindOfSport? KindOfSport,
        University? University,
        Coach? Coach);
}
