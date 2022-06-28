using JwtWebApiTutorial.Responses.SubmissionLeave;

namespace JwtWebApiTutorial.Responses.Submission
{
    public class GetHistorySubmissionResponse
    {
        public int? SubmissionId { get; set; }
        public int UserId { get; set; }
        public int OvertimeId { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime DatePerform { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string SubmissionType { get; set; }
        public string Description { get; set; }
        public string? Attachment { get; set; }
        public string StatusSubmission { get; set; }
        public List<GetHistoryApprovalResponse> HistoryApprovals { get; set; }
    }
}
