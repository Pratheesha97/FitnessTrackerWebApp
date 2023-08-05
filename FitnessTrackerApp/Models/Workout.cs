namespace FitnessTrackerApp.Models
{
    public class Workout
    {
        public int WorkoutId { get; set; } = 0;
        public string Name { get; set; } = "Running";
        public int CalorieExpenditurePerMin { get; set; } = 15;

    }
}
