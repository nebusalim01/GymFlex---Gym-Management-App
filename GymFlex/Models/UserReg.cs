using System.ComponentModel.DataAnnotations;

namespace GymFlex.Models
{
    public class UserReg
    {
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required]
        [Range(10, 100, ErrorMessage = "Enter valid age")]
        public int Age { get; set; }

        [Required]
        public int Height { get; set; } // in cm

        [Required]
        public int Weight { get; set; } // in kg

        public double BMI { get; set; }

        [Required]
        public string? Goal { get; set; }  // Weight Loss / Gain / Maintain

        [Required]
        public string? Activity_Level { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
