namespace Competitions.Models
{
    public class University
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Team> Teams { get; set; } = new();
    }
}
