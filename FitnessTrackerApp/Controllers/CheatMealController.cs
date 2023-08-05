using FitnessTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FitnessTrackerApp.Controllers
{
    public class CheatMealController : Controller
    {
        private static List<CheatMeal> cheatMeals = new List<CheatMeal> { new CheatMeal() };

        public IActionResult Index()
        {
            IEnumerable<CheatMeal> cheatMealList = cheatMeals;
            return PartialView("_AddCheatMeal", cheatMealList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CheatMeal obj)
        {
            if (ModelState.IsValid)
            {
                obj.MealId = cheatMeals.Count + 1;
                cheatMeals.Add(obj);
                TempData["success"] = "Cheat Meal created successfully";
                return RedirectToAction("Index");
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

            var cheatMeal = cheatMeals.FirstOrDefault(c => c.MealId == id);

            if (cheatMeal == null)
            {
                return NotFound();
            }

            return View(cheatMeal);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CheatMeal obj)
        {
            if (ModelState.IsValid)
            {
                var cheatMeal = cheatMeals.FirstOrDefault(c => c.MealId == obj.MealId);

                if (cheatMeal == null)
                {
                    return NotFound();
                }

                cheatMeal.MealName = obj.MealName;
                cheatMeal.CaloriesPerGram = obj.CaloriesPerGram;

                TempData["success"] = "Cheat Meal updated successfully";
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

            var cheatMeal = cheatMeals.FirstOrDefault(c => c.MealId == id);

            if (cheatMeal == null)
            {
                return NotFound();
            }

            return View(cheatMeal);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var cheatMeal = cheatMeals.FirstOrDefault(c => c.MealId == id);

            if (cheatMeal == null)
            {
                return NotFound();
            }

            cheatMeals.Remove(cheatMeal);
            TempData["success"] = "Cheat Meal deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

