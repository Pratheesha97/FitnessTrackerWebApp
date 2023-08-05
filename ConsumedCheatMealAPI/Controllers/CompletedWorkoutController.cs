using Microsoft.AspNetCore.Mvc;
using CompletedWorkoutAPI.Models;
using CompletedWorkoutAPI.Services;

namespace CompletedWorkoutAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedWorkoutController : Controller
    {
        private readonly ICompletedWorkoutService workoutService;
            
        public CompletedWorkoutController(ICompletedWorkoutService _workoutService)
        {
            workoutService = _workoutService;
        }
            
        [HttpGet]
        public IEnumerable<CompletedWorkout> WorkoutList()
        {
            var workoutList = workoutService.GetWorkoutList(0);
            return workoutList;
        }
            
        [HttpGet("{id}")]
        public CompletedWorkout GetWorkoutById(int id)
        {
            return workoutService.GetWorkoutById(id);
        }
            
        [HttpPost]
        public CompletedWorkout AddWorkout(CompletedWorkout workout)
        {
            return workoutService.AddWorkout(workout);
        }
            
        [HttpPut]
        public CompletedWorkout UpdateWorkout(CompletedWorkout workout)
        {
            return workoutService.UpdateWorkout(workout);
        }
            
        [HttpDelete("{id}")]
        public bool DeleteWorkout(int id)
        {
            return workoutService.DeleteWorkout(id);
        }
    }

}
