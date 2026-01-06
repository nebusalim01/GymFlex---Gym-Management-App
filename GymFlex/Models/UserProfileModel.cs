namespace GymFlex.Models
{
    public class UserProfileModel
    {
        public int User_Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public double BMI { get; set; }
        public string? Goal { get; set; }
        public string? Activity_Level { get; set; }

        public string? WorkoutPlanName { get; set; }
        public string? DietGoal { get; set; }
        public List<string> TodayExercises { get; set; } = new();
        public List<string> TodayMeals { get; set; } = new();
    }
}
