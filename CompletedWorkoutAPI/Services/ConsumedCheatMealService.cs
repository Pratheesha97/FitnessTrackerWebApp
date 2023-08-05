using ConsumedCheatMealAPI.Data;
using ConsumedCheatMealAPI.Models;

namespace ConsumedCheatMealAPI.Services
{
    public class ConsumedCheatMealService : IConsumedCheatMealService
    {
        private readonly ApplicationDbContext _dbContext;

        public ConsumedCheatMealService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ConsumedCheatMeal AddCheatMeal(ConsumedCheatMeal cheatMeal)
        {
            var result = _dbContext.ConsumedCheatMeals.Add(cheatMeal);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteCheatMeal(int Id)
        {
            var filteredData = _dbContext.ConsumedCheatMeals.Where(x => x.ConsumedCheatMealId == Id).FirstOrDefault();
            var result = _dbContext.Remove(filteredData);
            _dbContext.SaveChanges();
            return result != null ? true : false;
        }

        public IEnumerable<ConsumedCheatMeal> GetCheatMealList(int UserId)
        {
           return _dbContext.ConsumedCheatMeals.ToList();
        }

        public ConsumedCheatMeal GetCheatMealById(int id)
        {
            return _dbContext.ConsumedCheatMeals.Where(x => x.ConsumedCheatMealId == id).FirstOrDefault();
        }

        public ConsumedCheatMeal UpdateCheatMeal(ConsumedCheatMeal cheatMeal)
        {
            var result = _dbContext.ConsumedCheatMeals.Update(cheatMeal);
            _dbContext.SaveChanges();
            return result.Entity;
        }
    }
}
