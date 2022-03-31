namespace JwtWebApiTutorial.Requests.Attendance
{
    public class PostCheckoutOfflineRequest
    {
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
        public string Location { get; set; }
        public DateTime CheckoutTime { get; set; }
        public string Description { get; set; }
    }
}
