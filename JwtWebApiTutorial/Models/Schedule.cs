namespace JwtWebApiTutorial.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartCheckinAt { get; set; }
        public DateTime EndCheckinAt { get; set; }
        public DateTime StartCheckoutAt { get; set; }
        public DateTime EndCheckoutAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
