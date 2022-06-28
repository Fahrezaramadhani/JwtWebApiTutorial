namespace JwtWebApiTutorial.Requests.Submission
{
    public class PutApprovalSubmissionRequest
    {
        public int ApprovalId { get; set; }
        public int UserId { get; set; }
        public int SubmissionAttributeId { get; set; }
        public string StatusApproval { get; set; }
        public DateTime DateApproval { get; set; }
    }
}
