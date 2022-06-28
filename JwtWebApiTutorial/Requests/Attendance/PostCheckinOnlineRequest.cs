namespace JwtWebApiTutorial.Requests.Attendance
{
    public class PostCheckinOnlineRequest
    {
        public int UserId { get; set; }
        public string Location { get; set; }
        public string PhotoName { get; set; }
        public DateTime CheckinTime { get; set; }
        public string Description { get; set; }
    }
}
