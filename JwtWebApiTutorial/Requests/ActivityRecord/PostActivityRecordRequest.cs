namespace JwtWebApiTutorial.Requests.ActivityRecord
{
    public class PostActivityRecordRequest
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string PhotoName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
