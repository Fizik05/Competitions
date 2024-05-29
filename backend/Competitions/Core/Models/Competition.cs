using System.ComponentModel.DataAnnotations.Schema;

namespace Competitions.Core.Models
{
    public class Competition
    {
        private Competition(int id, string name, string description, int kindOfSportId, KindOfSport kindOfSport)
        {
            Id = id;
            Name = name;
            Description = description;
            KindOfSportId = kindOfSportId;
            KindOfSport = kindOfSport;
        }

        public int Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public int KindOfSportId { get; private set; }
        [ForeignKey(nameof(KindOfSportId))]
        public KindOfSport? KindOfSport { get; set; }

        public List<Team> Teams { get; set; } = [];

        public static (Competition competition, string Error) Create(int id, string name, string description, int kindOfSportId, KindOfSport? kindOfSport)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                error = "The Name field can't be empty";
            }

            if (string.IsNullOrEmpty(description))
            {
                error = "The Description field can't be empty";
            }

            var competition = new Competition(id, name, description, kindOfSportId, kindOfSport);

            return (competition, error);
        }
    }
}
