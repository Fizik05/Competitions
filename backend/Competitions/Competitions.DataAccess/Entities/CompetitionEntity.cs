using System.ComponentModel.DataAnnotations.Schema;

namespace Competitions.DataAccess.Entities
{
    public class CompetitionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int KindOfSportId { get; set; }
        [ForeignKey(nameof(KindOfSportId))]
        public KindOfSportEntity? KindOfSport { get; set; }

        public List<TeamEntity> Teams { get; set; } = [];
    }
}
