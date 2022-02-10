namespace JwtWebApiTutorial.Requests.Attendance
{
    public class PostCheckinRequest
    {
        public string location { get; set; }
        public string photo_name { get; set; }
        public DateTime checkin { get; set; }
        public string description { get; set; }
    }
}
