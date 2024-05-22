namespace Competitions.Core.Models
{
    public class University
    {
        private University(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; private set; } = string.Empty;

        public static (University university, string error) Create(int id, string name)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                error = "The Name field can't be empty";
            }

            var university = new University(id, name);

            return (university, error);
        }
    }
}
