using System.ComponentModel.DataAnnotations.Schema;

namespace Competitions.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int SportTypeId { get; set; }
        [ForeignKey(nameof(SportTypeId))]
        public SportType? SportType { get; set; }

        public int UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public University? University { get; set; }

        public int CoachId { get; set; }
        [ForeignKey(nameof(CoachId))]
        public Coach? Coach { get; set; }

        public List<Student> Students { get; set; } = new();
    }
}
