using Microsoft.EntityFrameworkCore;
using CompletedWorkoutAPI.Models;

namespace CompletedWorkoutAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<CompletedWorkout> CompletedWorkouts { get; set; }
    }
}
