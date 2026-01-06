using System.ComponentModel.DataAnnotations;

namespace GymFlex.Models
{
    public class EditUserProfileModel
    {
        public int User_Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [Range(10, 100)]
        public int Age { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Weight { get; set; }

        public float BMI { get; set; }

        [Required]
        public string? Goal { get; set; }

        [Required]
        public string? Activity_Level { get; set; }
    }
}
