namespace JwtWebApiTutorial.Responses.Submission
{
    public class GetApprovalSubmissionResponse
    {
        public int? SubmissionId { get; set; }
        public int SubmissionAttributeId { get; set; }
        public int ApprovalId { get; set; }
        public int OvertimeId { get; set; }
        public string UserName { get; set; }
        public string Position { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime DatePerform { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string SubmissionType { get; set; }
        public string Description { get; set; }
        public string? Attachment { get; set; }
        public string StatusSubmission { get; set; }
        public int UserIdApproval { get; set; }
        public string StatusApproval { get; set; }
        public DateTime DateApproval { get; set; }
    }
}
