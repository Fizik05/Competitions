using System.ComponentModel.DataAnnotations.Schema;

namespace Competitions.Core.Models
{
    public class Team
    {
        private Team(int id, string name, int kindOfSportId, int universityId, int coachId)
        {
            Id = id;
            Name = name;
            KindOfSportId = kindOfSportId;
            UniversityId = universityId;
            CoachId = coachId;
        }

        public int Id { get; set; }
        public string Name { get; private set; } = string.Empty;

        public int KindOfSportId { get; private set; }
        [ForeignKey(nameof(KindOfSportId))]
        public KindOfSport? KindOfSport { get; set; }

        public int UniversityId { get; private set; }
        [ForeignKey(nameof(UniversityId))]
        public University? University { get; set; }

        public int CoachId { get; private set; }
        [ForeignKey(nameof(CoachId))]
        public Coach? Coach { get; private set; }

        public List<Competition> Competitions { get; set; } = [];

        public static (Team team, string error) Create(int id, string name, int kindOfSportId, int universityId, int coachId)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                error = "The Name field can't be empty";
            }

            var team = new Team(id, name, kindOfSportId, universityId, coachId);

            return (team, error);
        } 
    }
}
