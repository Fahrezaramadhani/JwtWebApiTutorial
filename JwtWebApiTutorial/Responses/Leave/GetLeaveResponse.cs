namespace JwtWebApiTutorial.Responses.Leave
{
    public class GetLeaveResponse
    {
        public int LeaveId { get; set; }
        public int UserId { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
        public string StatusLeave { get; set; }
        public string UserNameApproval1 { get; set; }
        public string UserNameApproval2 { get; set; }
        public string UserNameApproval3 { get; set; }
        public string StatusApproval1 { get; set; }
        public string StatusApproval2 { get; set; }
        public string StatusApproval3 { get; set; }
        public DateTime DateApproval1 { get; set; }
        public DateTime DateApproval2 { get; set; }
        public DateTime DateApproval3 { get; set; }
    }
}
