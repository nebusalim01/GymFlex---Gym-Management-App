using System.ComponentModel.DataAnnotations;

namespace GymFlex.Models
{
    public class WorkoutExerciseModel
    {
        public int Exercise_Id { get; set; }

        [Required]
        public int Workoutday_Id { get; set; }

        [Required(ErrorMessage = "Exercise name is required")]
        public string? Exercise_Name { get; set; }

        [Required(ErrorMessage = "Sets required")]
        [Range(1, 20, ErrorMessage = "Sets must be between 1 and 20")]
        public int Sets { get; set; }

        [Required(ErrorMessage = "Reps required")]
        public string? Reps { get; set; }

        [Required(ErrorMessage = "Rest time required")]
        [Range(10, 600, ErrorMessage = "Rest must be 10–600 seconds")]
        public int Rest_Seconds { get; set; }
    }
}
