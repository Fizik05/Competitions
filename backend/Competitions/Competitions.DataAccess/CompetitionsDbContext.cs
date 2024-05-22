using Competitions.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Competitions.DataAccess
{
    public class CompetitionsDbContext: DbContext
    {
        public CompetitionsDbContext(DbContextOptions options) : base(options)
        {
            /*Database.EnsureDeleted();*/
            Database.EnsureCreated();
        }

        public DbSet<CompetitionEntity> Competitions { get; set; }
        public DbSet<KindOfSportEntity> KindOfSports { get; set; }
        public DbSet<CoachEntity> Coaches { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<UniversityEntity> Universities { get; set; }
    }
}
