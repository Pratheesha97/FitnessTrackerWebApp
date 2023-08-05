using FitnessTrackerApp.Models;
using FitnessTrackerApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTrackerApp.Controllers
{
    public class CompletedWorkoutController : Controller
    {
        private IAPIClientService<CompletedWorkout> _iAPIClientService;
        private readonly string _subURL = "completedWorkout";
        private readonly string _subURLwithSlash = "completedWorkout/";

        public CompletedWorkoutController(IAPIClientService<CompletedWorkout> iAPIClientService)
        {
            _iAPIClientService = iAPIClientService;
        }

        public async Task<IActionResult> Index()
        {
            var workouts = await _iAPIClientService.GetAll(_subURL);
            return PartialView("_CompletedWorkoutIndex", workouts);
                    
        }

        public async Task<IActionResult> Details(Int64 id)
        {
            var workout = await _iAPIClientService.GetById(id, _subURLwithSlash);
            return View(workout);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompletedWorkout workout)
        {
            if (ModelState.IsValid)
            {
                var result = await _iAPIClientService.Add(workout, _subURL);
                TempData["success"] = "Workout Log added successfully";
                return Redirect("/#logActivityView");
            }
            return View(workout);
        }

        public async Task<IActionResult> Edit(Int64 id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var workout = await _iAPIClientService.GetById(id, _subURLwithSlash);

            //CompletedWorkout completedWorkoutObj = (CompletedWorkout)workout;

            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompletedWorkout workout)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resut = await _iAPIClientService.Update(workout, _subURL);
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Error when updating completed workout data";
                    throw ex;
                }
                TempData["success"] = "Workout Log updated successfully";
                return Redirect("/#logActivityView");
            }
            return View(workout);
        }
        public async Task<IActionResult> Delete(Int64 id)
        {
            var workout = await _iAPIClientService.GetById(id, _subURLwithSlash);
            return View(workout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(CompletedWorkout workout)
        {
            var resut = await _iAPIClientService.Delete(workout.CompletedWorkoutId, _subURLwithSlash);
            TempData["success"] = "Workout Log deleted successfully";
            return Redirect("/#logActivityView");
        }
    }
}
