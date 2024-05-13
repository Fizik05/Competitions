using System.ComponentModel.DataAnnotations.Schema;

namespace Competitions.Models
{
    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int SportTypeId { get; set; }
        [ForeignKey(nameof(SportTypeId))]
        public SportType? SportType { get; set; }
    }
}
