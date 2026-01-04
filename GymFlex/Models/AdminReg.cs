using System.ComponentModel.DataAnnotations;

namespace GymFlex.Models
{
    public class AdminReg
    {
        public int Admin_Id { set; get; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { set; get; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid phone number")]
        public string? Phone { set; get; }

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
