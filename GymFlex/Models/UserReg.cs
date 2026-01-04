using System.ComponentModel.DataAnnotations;

namespace GymFlex.Models
{
    public class UserReg
    {
        public int User_Id { set; get; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { set; get; }

        [Required(ErrorMessage = "Age is required")]
        [Range(12, 80, ErrorMessage = "Age must be between 12 and 80")]
        public int Age { set; get; }

        [Required(ErrorMessage = "Height is required")]
        [Range(100, 250, ErrorMessage = "Height must be between 100 and 250 cm")]
        public int Height { set; get; }   // cm

        [Required(ErrorMessage = "Weight is required")]
        [Range(20, 300, ErrorMessage = "Weight must be between 20 and 300 kg")]
        public int Weight { set; get; }   // kg

        public float BMI { set; get; }    // calculated automatically

        [Required(ErrorMessage = "Please select a goal")]
        public string? Goal { set; get; }

        [Required(ErrorMessage = "Please select activity level")]
        public string? Activity_Level { set; get; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string? Email { set; get; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { set; get; }

        [DataType(DataType.Password)]

        [Required(ErrorMessage = "Reenter the Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { set; get; }

        public DateTime RegistrationDate { set; get; } = DateTime.Now;
    }
}
