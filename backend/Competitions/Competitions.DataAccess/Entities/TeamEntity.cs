using System.ComponentModel.DataAnnotations.Schema;

namespace Competitions.DataAccess.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int KindOfSportId { get; set; }
        [ForeignKey(nameof(KindOfSportId))]
        public KindOfSportEntity? KindOfSport { get; set; }

        public int UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public UniversityEntity? University { get; set; }

        public int CoachId { get; set; }
        [ForeignKey(nameof(CoachId))]
        public CoachEntity? Coach { get; set; }

        public List<CompetitionEntity> Competitions { get; set; } = [];
    }
}
