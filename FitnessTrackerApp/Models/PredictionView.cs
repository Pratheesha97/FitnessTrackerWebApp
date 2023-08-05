namespace FitnessTrackerApp.Models
{
    public class PredictionView
    {
        public DateTime FutureDate { get; set; }
        public string PredictedFitnessStatus { get; set; }
        public double PredictedWeight { get; set; }
        public List<string> ChartLabels { get; set; } = new List<string>();
        public List<double> ChartWeights { get; set; } = new List<double>();
        public List<string> ChartFitnessStatus { get; set; } = new List<string>();
    }
}
