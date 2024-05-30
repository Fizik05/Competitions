namespace Competitions.Core.Models
{
    public class User
    {
        public User(int id, string username, string passwordHash, string email)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            Email = email;
        }

        public int Id { get; set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string Email { get; private set; }

        public static User Create(int id, string username, string passwordHash, string email)
        {
            return new User(id, username, passwordHash, email);
        }
    }
}
