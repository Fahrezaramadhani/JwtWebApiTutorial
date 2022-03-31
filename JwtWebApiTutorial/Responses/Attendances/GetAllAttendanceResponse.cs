namespace JwtWebApiTutorial.Responses.Attendances
{
    public class GetAllAttendanceResponse
    {
        public int Id { get; set; }
        public Boolean IsLate { get; set; }
        public DateTime CheckinAt { get; set; }
        public string LocationCheckin { get; set; } = string.Empty;
        public string DescriptionCheckin { get; set; } = string.Empty;
        public DateTime CheckoutAt { get; set; }
        public string LocationCheckout { get; set; } = string.Empty;
        public string DescriptionCheckout { get; set; } = string.Empty;
        public string PhotoName { get; set; } = string.Empty;
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
    }
}
