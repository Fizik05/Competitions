using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Competitions.Core.Models
{
    public class Coach
    {
        private Coach(int id, string name, string surname, DateTime dateOfBirth)
        {
            Id = id;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
        }

        public int Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public string Surname { get; private set; } = string.Empty;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; private set; } = DateTime.Now;

        public static (Coach coach, string error) Create(int id, string name, string surname, DateTime dateOfBirth)
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

            var coach = new Coach(id, name, surname, DateTime.SpecifyKind(dateOfBirth, DateTimeKind.Utc));

            return (coach, error);
        }
    }
}
