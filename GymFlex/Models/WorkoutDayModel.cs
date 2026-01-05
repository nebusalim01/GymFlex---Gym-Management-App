using System.ComponentModel.DataAnnotations;

namespace GymFlex.Models
{
    public class WorkoutDayModel
    {
        public int Workoutday_Id { get; set; }

        [Required]
        public int Plan_Id { get; set; }

        [Required(ErrorMessage = "Day number is required")]
        [Range(1, 100, ErrorMessage = "Day number must be between 1 and 100")]
        public int Day_Number { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Notes are required")]
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }
    }
}
