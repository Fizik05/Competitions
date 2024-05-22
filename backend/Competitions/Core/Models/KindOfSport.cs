using System.ComponentModel.DataAnnotations;

namespace Competitions.Core.Models
{
    public class KindOfSport
    {
        private KindOfSport(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; private set; } = string.Empty;

        public static (KindOfSport kindOfSport, string error) Create(int id, string name)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                error = "The Name field can't be empty";
            }

            var kindOfSport = new KindOfSport(id, name);

            return (kindOfSport, error);
        }
    }
}
