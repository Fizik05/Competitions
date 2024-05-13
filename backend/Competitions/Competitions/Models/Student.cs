using System.ComponentModel.DataAnnotations.Schema;

namespace Competitions.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int TeamId { get; set; }
        [ForeignKey(nameof(TeamId))]
        public Team? Team { get; set; }
    }
}
