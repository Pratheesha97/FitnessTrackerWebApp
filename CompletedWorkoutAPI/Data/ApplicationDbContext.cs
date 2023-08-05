using Microsoft.EntityFrameworkCore;
using ConsumedCheatMealAPI.Models;

namespace ConsumedCheatMealAPI.Data
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

        public DbSet<ConsumedCheatMeal> ConsumedCheatMeals { get; set; }
    }
}
