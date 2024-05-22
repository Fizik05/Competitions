namespace Competitions.Contracts.Students
{
    public record StudentRequest(
        string Name,
        string Surname,
        DateTime DateOfBirth,
        int TeamId);
}
