namespace GymFlex.Models
{
    public class UserListModel
    {
        public int User_Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public float BMI { get; set; }
        public string? Goal { get; set; }
        public string? Activity_Level { get; set; }
    }
}
