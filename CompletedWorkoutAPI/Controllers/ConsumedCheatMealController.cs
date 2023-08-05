using Microsoft.AspNetCore.Mvc;
using ConsumedCheatMealAPI.Models;
using ConsumedCheatMealAPI.Services;

namespace CheatMealAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumedCheatMealController : Controller
    {
        private readonly IConsumedCheatMealService cheatMealService;
            
        public ConsumedCheatMealController(IConsumedCheatMealService _cheatMealService)
        {
            cheatMealService = _cheatMealService;
        }
            
        [HttpGet]
        public IEnumerable<ConsumedCheatMeal> CheatMealList()
        {
            var cheatMealList = cheatMealService.GetCheatMealList(0);
            return cheatMealList;
        }
            
        [HttpGet("{id}")]
        public ConsumedCheatMeal GetCheatMealById(int id)
        {
            return cheatMealService.GetCheatMealById(id);
        }
            
        [HttpPost]
        public ConsumedCheatMeal AddCheatMeal(ConsumedCheatMeal cheatMeal)
        {
            return cheatMealService.AddCheatMeal(cheatMeal);
        }
            
        [HttpPut]
        public ConsumedCheatMeal UpdateCheatMeal(ConsumedCheatMeal cheatMeal)
        {
            return cheatMealService.UpdateCheatMeal(cheatMeal);
        }
            
        [HttpDelete("{id}")]
        public bool DeleteCheatMeal(int id)
        {
            return cheatMealService.DeleteCheatMeal(id);
        }
    }

}
