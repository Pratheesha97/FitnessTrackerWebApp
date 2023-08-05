namespace FitnessTrackerApp.Models
{
    public class WeeklyReport
    {
        public DateTime WeekStartDate { get; set; }
        public IEnumerable<CompletedWorkout> Workouts { get; set; }
        public IEnumerable<ConsumedCheatMeal> CheatMeals { get; set; }
    }

}
