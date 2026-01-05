using System.ComponentModel.DataAnnotations;

namespace GymFlex.Models
{
    public class AssignPlanModel
    {
        [Required]
        public int User_Id { get; set; }

        [Required]
        public int Plan_Id { get; set; }

        [Required]
        public int Diet_Id { get; set; }

        public DateTime Assigned_Date { get; set; }
    }
}
