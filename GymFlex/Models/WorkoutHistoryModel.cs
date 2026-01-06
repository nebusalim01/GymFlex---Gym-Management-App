namespace GymFlex.Models
{
    public class WorkoutHistoryModel
    {
        public string? ExerciseName { get; set; }
        public int DurationSeconds { get; set; }
        public int SetsCompleted { get; set; }
        public DateTime CompletedDate { get; set; }
    }
}
