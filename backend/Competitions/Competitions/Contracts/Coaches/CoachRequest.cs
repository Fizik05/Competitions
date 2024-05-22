namespace Competitions.Contracts.Coaches
{
    public record CoachRequest(
        string Name,
        string Surname,
        DateTime DateOfBirth);
}
