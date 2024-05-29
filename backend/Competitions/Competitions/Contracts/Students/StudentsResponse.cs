using Competitions.Core.Models;

namespace Competitions.Contracts.Students
{
    public record StudentsResponse(
        int Id,
        string Name,
        string Surname,
        DateTime DateOfBirth,
        Team Team);
}
