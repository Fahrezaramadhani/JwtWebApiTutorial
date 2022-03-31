namespace JwtWebApiTutorial.Requests.Schedule
{
    public class PostScheduleRequest
    {
        public string Name { get; set; }
        public DateTime StartCheckinAt { get; set; }
        public DateTime EndCheckinAt { get; set; }
        public DateTime StartCheckoutAt { get; set; }
        public DateTime EndCheckoutAt { get; set; }
    }
}
