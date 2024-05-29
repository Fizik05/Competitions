using System.ComponentModel.DataAnnotations.Schema;

namespace Competitions.Core.Models
{
    public class Team
    {
        private Team(int id, string name, int kindOfSportId, int universityId, int coachId, KindOfSport kindOfSport, University university, Coach coach)
        {
            Id = id;
            Name = name;
            KindOfSportId = kindOfSportId;
            UniversityId = universityId;
            CoachId = coachId;
            KindOfSport = kindOfSport;
            University = university;
            Coach = coach;
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
        public Coach? Coach { get; set; }

        public List<Competition> Competitions { get; set; } = [];

        public static (Team team, string error) Create(int id, string name, int kindOfSportId, int universityId, int coachId, KindOfSport? kindOfSport, University? university, Coach? coach)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                error = "The Name field can't be empty";
            }

            var team = new Team(id, name, kindOfSportId, universityId, coachId, kindOfSport, university, coach);

            return (team, error);
        } 
    }
}
