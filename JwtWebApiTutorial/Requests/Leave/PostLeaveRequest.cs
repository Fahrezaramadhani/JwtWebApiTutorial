namespace JwtWebApiTutorial.Requests.Leave
{
    public class PostLeaveRequest
    {
        public int UserId { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
    }
}
