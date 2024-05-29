using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Competitions.Core.Models
{
    public class Student
    {
        private Student(int id, string name, string surname, DateTime dateOfBirth, int teamId, Team? team)
        {
            Id = id;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            TeamId = teamId;
            Team = team;
        }

        public int Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public string Surname { get; private set; } = string.Empty;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; private set; } = DateTime.Now;

        public int TeamId { get; private set; }
        [ForeignKey(nameof(TeamId))]
        public Team? Team { get; set; }

        public static (Student student, string error) Create(int id, string name, string surname, DateTime dateOfBirth, int teamId, Team? team)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                error = "The Name field can't be empty";
            }

            if (string.IsNullOrEmpty(surname))
            {
                error = "The Name field can't be empty";
            }

            var student = new Student(id, name, surname, DateTime.SpecifyKind(dateOfBirth, DateTimeKind.Utc), teamId, team);

            return (student, error);
        }
    }
}
