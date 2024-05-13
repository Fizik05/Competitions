using System.ComponentModel.DataAnnotations.Schema;

namespace Competitions.Models
{
    public class CompetitionTeam
    {
        public int Id { get; set; }

        public int CompetitionId { get; set; }
        [ForeignKey(nameof(CompetitionId))]
        public Competition? Competition { get; set; }

        public int TeamId { get; set; }
        [ForeignKey(nameof(TeamId))]
        public Team? Team { get; set; }
    }
}
