using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTrackerApp.Models;
using FitnessTrackerApp.Services;

namespace FitnessTrackerApp.Controllers
{
    public class PredictionViewController : Controller
    {
        private IAPIClientService<ConsumedCheatMeal> _iAPIClientServiceConsumedCheatMeal;
        private IAPIClientService<CompletedWorkout> _iAPIClientServiceCompletedWorkout;
        private readonly string _subURL = "completedWorkout";

        public PredictionViewController(IAPIClientService<ConsumedCheatMeal> iAPIClientServiceConsumedCheatMeal, IAPIClientService<CompletedWorkout> iAPIClientServiceCompletedWorkout)
        {
            _iAPIClientServiceConsumedCheatMeal = iAPIClientServiceConsumedCheatMeal;
            _iAPIClientServiceCompletedWorkout = iAPIClientServiceCompletedWorkout;
        }

        [HttpGet]
        public IActionResult PredictFitness()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PredictFitness(DateTime futureDate)
        {
            // Retrieve historical workout and cheat meal data from the database
            var completedWorkouts = await _iAPIClientServiceCompletedWorkout.GetAll("CompletedWorkout");
            var cheatMeals = await _iAPIClientServiceConsumedCheatMeal.GetAll("ConsumedCheatMeal");

            // Check if the historical data is null or empty
            if (completedWorkouts == null || completedWorkouts.Count == 0 ||
                cheatMeals == null || cheatMeals.Count == 0)
            {
                // Handle the case where data is missing or empty.
                // You can either proceed with default values or simply show a message to the user.
                TempData["WarningMessage"] = "Historical data not available. Using default values for prediction.";
            }

            // Set default values for weightTrend, durationTrend, and cheatMealsTrend in case of missing data.
            double weightTrend = 0.0;
            double durationTrend = 0.0;
            double cheatMealsTrend = 0.0;

            // Calculate the start date for the historical data (14 days ago from the future date)
            var startDate = futureDate.AddDays(-14);

            // Check if completedWorkouts and cheatMeals have data to calculate trends
            if (completedWorkouts != null && completedWorkouts.Count > 0)
            {
                weightTrend = CalculateWeightTrend(completedWorkouts, startDate, futureDate);
                durationTrend = CalculateDurationTrend(completedWorkouts, startDate, futureDate);
            }

            if (cheatMeals != null && cheatMeals.Count > 0)
            {
                cheatMealsTrend = CalculateCheatMealsTrend(cheatMeals, startDate, futureDate);
            }

            // Calculate the average weight and duration if historical data is available
            int estimatedWeight = completedWorkouts != null && completedWorkouts.Count > 0
                ? (int)(completedWorkouts.Last().PostWorkoutWeight + weightTrend)
                : 0;

            int estimatedDuration = completedWorkouts != null && completedWorkouts.Count > 0
                ? (int)(completedWorkouts.Last().Duration + durationTrend)
                : 0;

            // Calculate the fitness status based on the predicted weight
            var predictedFitnessStatus = GetFitnessStatus(estimatedWeight);

            var viewModel = new PredictionView
            {
                FutureDate = futureDate,
                PredictedFitnessStatus = predictedFitnessStatus,
                PredictedWeight = estimatedWeight,
                ChartLabels = completedWorkouts.Select(workout => workout.Date.ToShortDateString()).ToList(),
                ChartWeights = completedWorkouts.Select(workout => (double)workout.PostWorkoutWeight).ToList(),
                ChartFitnessStatus = Enumerable.Repeat(predictedFitnessStatus, 14).ToList()
        };

            return View(viewModel);
        }

        private double CalculateWeightTrend(List<CompletedWorkout> completedWorkouts, DateTime startDate, DateTime endDate)
        {
            // Calculate the change in weight over the given period
            double weightChange = completedWorkouts.Last().PostWorkoutWeight - completedWorkouts.First().PostWorkoutWeight;

            // Calculate the number of days in the period
            int days = (int)(endDate - startDate).TotalDays + 1;

            // Calculate the average weight change per day
            double weightTrend = weightChange / days;

            return weightTrend;
        }

        private double CalculateDurationTrend(List<CompletedWorkout> completedWorkouts, DateTime startDate, DateTime endDate)
        {
            // Calculate the change in workout duration over the given period
            double durationChange = completedWorkouts.Last().Duration - completedWorkouts.First().Duration;

            // Calculate the number of days in the period
            int days = (int)(endDate - startDate).TotalDays + 1;

            // Calculate the average duration change per day
            double durationTrend = durationChange / days;

            return durationTrend;
        }

        private double CalculateCheatMealsTrend(List<ConsumedCheatMeal> cheatMeals, DateTime startDate, DateTime endDate)
        {
            // Calculate the change in cheat meal count over the given period
            double cheatMealsChange = cheatMeals.Last().AmountConsumed - cheatMeals.First().AmountConsumed;

            // Calculate the number of days in the period
            int days = (int)(endDate - startDate).TotalDays + 1;

            // Calculate the average cheat meal count change per day
            double cheatMealsTrend = cheatMealsChange / days;

            return cheatMealsTrend;
        }

        private string GetFitnessStatus(double predictedWeight)
        {
            if (predictedWeight < 150)
            {
                return "Underweight";
            }
            else if (predictedWeight >= 150 && predictedWeight < 200)
            {
                return "Healthy";
            }
            else
            {
                return "Overweight";
            }
        }
    }
}
