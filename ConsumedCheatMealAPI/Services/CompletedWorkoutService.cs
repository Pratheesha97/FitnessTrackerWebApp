using CompletedWorkoutAPI.Data;
using CompletedWorkoutAPI.Models;

namespace CompletedWorkoutAPI.Services
{
    public class CompletedWorkoutService : ICompletedWorkoutService
    {
        private readonly ApplicationDbContext _dbContext;

        public CompletedWorkoutService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CompletedWorkout AddWorkout(CompletedWorkout workout)
        {
            var result = _dbContext.CompletedWorkouts.Add(workout);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteWorkout(int Id)
        {
            var filteredData = _dbContext.CompletedWorkouts.Where(x => x.CompletedWorkoutId == Id).FirstOrDefault();
            var result = _dbContext.Remove(filteredData);
            _dbContext.SaveChanges();
            return result != null ? true : false;
        }

        public IEnumerable<CompletedWorkout> GetWorkoutList(int UserId)
        {
           return _dbContext.CompletedWorkouts.ToList();
        }

        public CompletedWorkout GetWorkoutById(int id)
        {
            return _dbContext.CompletedWorkouts.Where(x => x.CompletedWorkoutId == id).FirstOrDefault();
        }

        public CompletedWorkout UpdateWorkout(CompletedWorkout workout)
        {
            var result = _dbContext.CompletedWorkouts.Update(workout);
            _dbContext.SaveChanges();
            return result.Entity;
        }
    }
}
