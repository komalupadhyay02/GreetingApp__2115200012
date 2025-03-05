using Microsoft.EntityFrameworkCore;
using ModelLayer.Entity;

namespace RepositoryLayer.Context
{
    public class GreetingDbContext : DbContext
    {
        public GreetingDbContext(DbContextOptions<GreetingDbContext> options) : base(options)
        {
        }

        // Define your DbSets
        public DbSet<Greeting> Greetings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "YourConnectionStringHere",
                    b => b.MigrationsAssembly("RepositoryLayer")
                );
            }
        }
    }
}
