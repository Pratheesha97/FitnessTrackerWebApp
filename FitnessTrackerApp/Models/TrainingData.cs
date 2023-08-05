namespace FitnessTrackerApp.Models
{
    public class TrainingData
    {
        public DateTime Date { get; set; }
        public float Duration { get; set; }
        public float CheatMealsCount { get; set; }
        public float TotalCheatMealAmount { get; set; }
        public float PostWorkoutWeight { get; set; }
        public string FitnessStatus { get; set; }
    }
}
