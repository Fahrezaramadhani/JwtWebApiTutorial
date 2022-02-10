namespace JwtWebApiTutorial.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        public string is_late { get; set; }
        public DateTime checkin_at { get; set; }
        public DateTime checkout_at { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
