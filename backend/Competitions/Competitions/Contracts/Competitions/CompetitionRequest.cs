namespace Competitions.Contracts.Competitions
{
    public record CompetitionRequest(
        string Name,
        string Description,
        int KindOfsportId);
}
