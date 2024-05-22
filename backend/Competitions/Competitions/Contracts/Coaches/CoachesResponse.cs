namespace Competitions.Contracts.Coaches
{
    public record CoachesResponse(
        int Id,
        string Name,
        string Surname,
        DateTime DateOfBirth);
}
