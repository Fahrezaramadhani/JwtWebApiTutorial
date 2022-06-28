namespace JwtWebApiTutorial.Responses.SubmissionLeave
{
    public class GetHistoryApprovalResponse
    {
        public int ApprovalId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string StatusApproval { get; set; }
        public DateTime DateApproval { get; set; }
    }
}
