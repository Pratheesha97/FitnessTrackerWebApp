using System.ComponentModel.DataAnnotations;

namespace ConsumedCheatMealAPI.Models
{
    public class ConsumedCheatMeal
    {
        [Key]
        public int ConsumedCheatMealId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int MealId { get; set; }
        public int AmountConsumed { get; set; }
        public int PostMealWeight { get; set; }
    }
}