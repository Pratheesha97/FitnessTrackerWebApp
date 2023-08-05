using ConsumedCheatMealAPI.Models;

namespace ConsumedCheatMealAPI.Services
{
    public interface IConsumedCheatMealService
    {
        public IEnumerable<ConsumedCheatMeal> GetCheatMealList(int UserId);
        public ConsumedCheatMeal GetCheatMealById(int id);
        public ConsumedCheatMeal AddCheatMeal(ConsumedCheatMeal cheatMeal);
        public ConsumedCheatMeal UpdateCheatMeal(ConsumedCheatMeal cheatMeal);
        public bool DeleteCheatMeal(int Id);
    }
}
