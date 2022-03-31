namespace JwtWebApiTutorial.Responses.Schedules
{
    public class GetAllScheduleResponse
    {
        public int ScheduleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartCheckinAt { get; set; }
        public DateTime EndCheckinAt { get; set; }
        public DateTime StartCheckoutAt { get; set; }
        public DateTime EndCheckoutAt { get; set; }
    }
}
