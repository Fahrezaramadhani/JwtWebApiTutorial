namespace JwtWebApiTutorial.Requests.Approval
{
    public class PostApprovalRequest
    {
        public int SubmissionAttributeId { get; set; }
        public int UserId { get; set; }
        public string StatusApproval { get; set; }
        public DateTime DateApproval { get; set; }
    }
}
