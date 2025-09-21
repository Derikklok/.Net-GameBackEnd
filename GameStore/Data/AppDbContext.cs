using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // This represents our table "Tasks"
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Officer> Officers { get; set; }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<GrandPrix> GrandPrix { get; set; }
        public DbSet<Participation> Participation { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite unique key to prevent duplicate assignments
            modelBuilder.Entity<Participation>()
                .HasIndex(p => new { p.DriverId, p.GrandPrixId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}


