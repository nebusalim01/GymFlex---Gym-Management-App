using System.ComponentModel.DataAnnotations;

namespace GymFlex.Models
{
    public class WorkoutPlanModel
    {
        public int Plan_Id { get; set; }

        [Required(ErrorMessage = "Plan name is required")]
        [StringLength(100, ErrorMessage = "Plan name cannot exceed 100 characters")]
        public string? Plan_Name { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days")]
        public int Duration_Days { get; set; }

        [Required(ErrorMessage = "Goal type is required")]
        public string? Goal_Type { get; set; }

        [Required(ErrorMessage = "Level is required")]
        public string? Level { get; set; }
    }
}
