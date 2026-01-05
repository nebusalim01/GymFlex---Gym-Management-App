using System.ComponentModel.DataAnnotations;

namespace GymFlex.Models
{
    public class DietPlanModel
    {
        public int Diet_Id { get; set; }

        [Required]
        public string? Goal_Type { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Day number must be between 1 and 100")]
        public int Day_Number { get; set; }

        [Required]
        public string? Meal_Type { get; set; }

        [Required]
        [StringLength(500)]
        public string? Food_Items { get; set; }

        [Required]
        [Range(50, 3000)]
        public int Calories { get; set; }
    }
}
