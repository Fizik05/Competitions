using System.ComponentModel.DataAnnotations;

namespace Competitions.Contracts.Users
{
    public record RegisterUserRequest(
        [Required] string Username,
        [Required] string Password,
        [Required] string Email);
}
