using Competitions.Models;
using Microsoft.EntityFrameworkCore;

namespace Competitions.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
        }

        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<SportType> SportTypes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<CompetitionTeam> CompetitionTeams { get; set; }
    }
}
