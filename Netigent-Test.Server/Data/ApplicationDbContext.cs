namespace Netigent_Test.Server.Data
{
    using Microsoft.EntityFrameworkCore;
    using Netigent_Test.Server.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding initial data
            modelBuilder.Entity<StatusLevel>().HasData(
                new StatusLevel { Id = 1, StatusName = "Approved" },
                new StatusLevel { Id = 2, StatusName = "In-Progress" },
                new StatusLevel { Id = 3, StatusName = "Paused" },
                new StatusLevel { Id = 4, StatusName = "Completed" }
            );
        }

        public DbSet<Application> Applications{ get; set; }
        public DbSet<Inquire> Inquiries { get; set; }
        public DbSet<StatusLevel> StatusLevels { get; set; }
    }
}
