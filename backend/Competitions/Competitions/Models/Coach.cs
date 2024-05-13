namespace Competitions.Models
{
    public class Coach
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<Team> Teams { get; set; } = new();
    }
}
