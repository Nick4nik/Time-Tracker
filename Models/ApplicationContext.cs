using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Time_Tracker.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=TimeTrackerDb;Trusted_Connection=True;");
        }
    }
}