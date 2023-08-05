using CompletedWorkoutAPI.Models;

namespace CompletedWorkoutAPI.Services
{
    public interface ICompletedWorkoutService
    {
        public IEnumerable<CompletedWorkout> GetWorkoutList(int UserId);
        public CompletedWorkout GetWorkoutById(int id);
        public CompletedWorkout AddWorkout(CompletedWorkout workout);
        public CompletedWorkout UpdateWorkout(CompletedWorkout workout);
        public bool DeleteWorkout(int Id);
    }
}
