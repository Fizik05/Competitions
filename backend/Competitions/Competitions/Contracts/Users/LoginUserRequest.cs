using System.ComponentModel.DataAnnotations;

namespace Competitions.Contracts.Users
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password);
}
