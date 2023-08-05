using FitnessTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FitnessTrackerApp.Controllers
{
    public class WorkoutController : Controller
    {
        private static List<Workout> workouts = new List<Workout> { new Workout() };

        public IActionResult Index()
        {
            IEnumerable<Workout> workoutList = workouts;
            return PartialView("_AddWorkout", workoutList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Workout obj)
        {
            if (ModelState.IsValid)
            {
                obj.WorkoutId = workouts.Count + 1;
                workouts.Add(obj);
                TempData["success"] = "Workout created successfully";
                return Redirect("/#workoutType");
            }
            return View(obj);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var workout = workouts.FirstOrDefault(w => w.WorkoutId == id);

            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Workout obj)
        {
            if (ModelState.IsValid)
            {
                var workout = workouts.FirstOrDefault(w => w.WorkoutId == obj.WorkoutId);

                if (workout == null)
                {
                    return NotFound();
                }

                workout.Name = obj.Name;
                workout.CalorieExpenditurePerMin = obj.CalorieExpenditurePerMin;

                TempData["success"] = "Workout updated successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var workout = workouts.FirstOrDefault(w => w.WorkoutId == id);

            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteWorkoutPOST(int? id)
        {
            var workout = workouts.FirstOrDefault(w => w.WorkoutId == id);

            if (workout == null)
            {
                return NotFound();
            }

            workouts.Remove(workout);
            TempData["success"] = "Workout deleted successfully";
            return Redirect("/#workoutType");
        }
    }
}
