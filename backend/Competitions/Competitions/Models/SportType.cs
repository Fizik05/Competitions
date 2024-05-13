namespace Competitions.Models
{
    public class SportType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Team> Teams { get; set; } = new();
        public List<Competition> Competitions { get; set; } = new();
    }
}
