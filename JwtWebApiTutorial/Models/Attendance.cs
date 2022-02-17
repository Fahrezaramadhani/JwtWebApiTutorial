namespace JwtWebApiTutorial.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public string IsLate { get; set; } 
        public DateTime CheckinAt { get; set; }
        public string LocationCheckin { get; set; }
        public string DescriptionCheckin { get; set; }
        public DateTime CheckoutAt { get; set; }
        public string LocationCheckout { get; set; }
        public string DescriptionCheckout { get; set; }
        public string PhotoName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;

        //Foreign Key for schedule id 
        public Schedule Schedule { get; set; }
        public int ScheduleId { get; set; }

        //Foreign key for user id
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
