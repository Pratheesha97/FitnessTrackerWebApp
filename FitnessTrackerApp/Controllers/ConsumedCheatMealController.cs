using FitnessTrackerApp.Models;
using FitnessTrackerApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTrackerApp.Controllers
{
    public class ConsumedCheatMealController : Controller
    {
        private IAPIClientService<ConsumedCheatMeal> _iAPIClientService;
        private readonly string _subURL = "consumedCheatMeal";
        private readonly string _subURLwithSlash = "consumedCheatMeal/";

        public ConsumedCheatMealController(IAPIClientService<ConsumedCheatMeal> iAPIClientService)
        {
            _iAPIClientService = iAPIClientService;
        }

        public async Task<IActionResult> Index()
        {
            var cheatMeals = await _iAPIClientService.GetAll(_subURL);
            return PartialView("_ConsumedCheatMealIndex", cheatMeals);
        }

        public async Task<IActionResult> Details(Int64 id)
        {
            var cheatMeal = await _iAPIClientService.GetById(id, _subURLwithSlash);
            return View(cheatMeal);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsumedCheatMeal cheatMeal)
        {
            if (ModelState.IsValid)
            {
                var resut = await _iAPIClientService.Add(cheatMeal, _subURL);
                TempData["success"] = "Cheat Meal Log added successfully";
                return Redirect("/#logActivityView");
            }
            return View(cheatMeal);
        }

        public async Task<IActionResult> Edit(Int64 id)
        {
            
           if (id == null || id == 0)
            {
                return NotFound();
            }

            var cheatMeal = await _iAPIClientService.GetById(id, _subURLwithSlash);

            

            if (cheatMeal == null)
            {
                return NotFound();
            }

            return View(cheatMeal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ConsumedCheatMeal cheatMeal)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resut = await _iAPIClientService.Update(cheatMeal, _subURL);
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Error when editing Cheat Meal data";
                    throw ex;
                }
                TempData["success"] = "Cheat Meal data updated successfully";
                return Redirect("/#logActivityView");
            }
            return View(cheatMeal);
        }
        public async Task<IActionResult> Delete(Int64 id)
        {
            var cheatMeal = await _iAPIClientService.GetById(id, _subURLwithSlash);
            return View(cheatMeal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(ConsumedCheatMeal cheatMeal)
        {
            var resut = await _iAPIClientService.Delete(cheatMeal.ConsumedCheatMealId, _subURLwithSlash);
            TempData["success"] = "Cheat Meal data deleted successfully";
            return Redirect("/#logActivityView");
        }
    }
}
