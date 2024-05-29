using Competitions.Core.Models;

namespace Competitions.Contracts.Competitions
{
    public record CompetitionsResponse(
        int Id,
        string Name,
        string Description,
        KindOfSport KindOfSport);
}
