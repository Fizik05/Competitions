using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Competitions.DataAccess.Entities
{
    public class StudentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        public int TeamId { get; set; }
        [ForeignKey(nameof(TeamId))]
        public TeamEntity? Team { get; set; }
    }
}
