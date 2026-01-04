namespace GymFlex.Models
{
    public class AdminProfileModel
    {
        public int AdminId { set; get; }
        public string? Name { set; get; }
        public string? Phone { set; get; }
        public string? Email { set;get; }
        public DateTime CreatedAt { set; get; }
    }
}
