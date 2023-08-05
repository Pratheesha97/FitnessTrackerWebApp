using System.ComponentModel.DataAnnotations;

namespace CompletedWorkoutAPI.Models
{
    public class CompletedWorkout
    {
        [Key]
        public int CompletedWorkoutId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int WorkoutId { get; set; }
        public int Duration { get; set; }
        public int PostWorkoutWeight { get; set; }
    }
}
