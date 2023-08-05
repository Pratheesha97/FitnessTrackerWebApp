namespace FitnessTrackerApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public double HeightInCm { get; set; }
        public double InitialWeightInKg { get; set; }
        public int RegularCalorieIntake { get; set; }
        public int RegularCalorieExpenditure { get; set; }

    }
}
