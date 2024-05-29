namespace Competitions.Contracts.Teams
{
    public record TeamRequest(
        string Name,
        int KindOfSportId,
        int UniversityId,
        int CoachId);
}
