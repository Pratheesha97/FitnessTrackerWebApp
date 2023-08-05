using FitnessTrackerApp.Models;
using FitnessTrackerApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessTrackerApp.Controllers
{
    public class WeeklyReportController : Controller
    {
        private IAPIClientService<ConsumedCheatMeal> _iAPIClientServiceConsumedCheatMeal;
        private IAPIClientService<CompletedWorkout> _iAPIClientServiceCompletedWorkout;

        public WeeklyReportController(IAPIClientService<ConsumedCheatMeal> iAPIClientServiceConsumedCheatMeal, IAPIClientService<CompletedWorkout> iAPIClientServiceCompletedWorkout)
        {
            _iAPIClientServiceConsumedCheatMeal = iAPIClientServiceConsumedCheatMeal;
            _iAPIClientServiceCompletedWorkout = iAPIClientServiceCompletedWorkout;
        }

        public async Task<IActionResult> GenerateReport()
        {
            // Get the current date and find the start date of the week (Sunday)
            DateTime currentDate = DateTime.Today;
            DateTime weekStartDate = currentDate.AddDays(-(int)currentDate.DayOfWeek);

            // Create a list to store the weekly reports
            List<WeeklyReport> weeklyReports = new List<WeeklyReport>();

            // Loop to generate reports for multiple weeks (past 4 weeks)
            int numberOfWeeksToShow = 4;
            for (int i = 0; i < numberOfWeeksToShow; i++)
            {
                // Get the workouts and cheat meals for the week
                var consumedCheatMeals = await _iAPIClientServiceConsumedCheatMeal.GetAll("ConsumedCheatMeal");
                var filteredCheatMeals = consumedCheatMeals.Where(item => item.Date >= weekStartDate && item.Date <= weekStartDate.AddDays(6)).ToList();
                IEnumerable<ConsumedCheatMeal> cheatmeals = filteredCheatMeals;

                var completedWorkouts = await _iAPIClientServiceCompletedWorkout.GetAll("CompletedWorkout");
                var filteredWorkouts = completedWorkouts.Where(item => item.Date >= weekStartDate && item.Date <= weekStartDate.AddDays(6)).ToList();
                IEnumerable<CompletedWorkout> workouts = filteredWorkouts;

                // Create a view model for the current week's report and add it to the list
                var viewModel = new WeeklyReport
                {
                    WeekStartDate = weekStartDate,
                    Workouts = workouts,
                    CheatMeals = cheatmeals
                };

                weeklyReports.Add(viewModel);

                // Move to the previous week
                weekStartDate = weekStartDate.AddDays(-7);
            }

            return View(weeklyReports);
        }
    }
}
