namespace JwtWebApiTutorial.Responses.SubmissionLeave
{
    public class GetHistoryLeaveResponse
    {
        public int? SubmissionLeaveId { get; set; }
        public int UserId { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string? Attachment { get; set; }
        public string StatusLeave { get; set; }
        public List<GetHistoryApprovalResponse> HistoryApprovals { get; set; }
    }
}
