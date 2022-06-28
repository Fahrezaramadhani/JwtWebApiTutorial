namespace JwtWebApiTutorial.Requests.Leave
{
    public class PutLeaveRequest
    {
        public int LeaveId { get; set; }
        public int UserId { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
        public int UserIdApproval1 { get; set; }
        public int UserIdApproval2 { get; set; }
        public int UserIdApproval3 { get; set; }
        public string StatusApproval1 { get; set; }
        public string StatusApproval2 { get; set; }
        public string StatusApproval3 { get; set; }
        public DateTime DateApproval1 { get; set; }
        public DateTime DateApproval2 { get; set; }
        public DateTime DateApproval3 { get; set; }
    }
}
