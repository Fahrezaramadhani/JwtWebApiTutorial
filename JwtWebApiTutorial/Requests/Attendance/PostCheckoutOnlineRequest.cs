namespace JwtWebApiTutorial.Requests.Attendance
{
    public class PostCheckoutOnlineRequest
    {
        public int UserId { get; set; }
        public string Location { get; set; }
        public DateTime CheckoutTime { get; set; }
        public string Description { get; set; }
    }
}
