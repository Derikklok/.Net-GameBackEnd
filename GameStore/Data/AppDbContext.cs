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
        public DbSet<Application> Applications{ get; set; }
        public DbSet<Officer> Officers{ get; set; }
    }
}


