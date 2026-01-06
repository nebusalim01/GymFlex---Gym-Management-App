namespace GymFlex.Models
{
    public class WorkoutProgressModel
    {
        public int User_Id { get; set; }
        public string? ExerciseName { get; set; }
        public int SetsPlanned { get; set; }
        public int SetsCompleted { get; set; }
        public int DurationSeconds { get; set; }
    }
}
