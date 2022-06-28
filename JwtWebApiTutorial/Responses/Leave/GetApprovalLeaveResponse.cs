using JwtWebApiTutorial.Models;

namespace JwtWebApiTutorial.Responses.Leave
{
    public class GetApprovalLeaveResponse
    {
        public int? SubmissionLeaveId { get; set; }
        public int SubmissionAttributeId { get; set; }
        public int ApprovalId { get; set; }
        public string UserName { get; set; }
        public string Position { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string? Attachment { get; set; }
        public string StatusLeave { get; set; }
        public int UserIdApproval { get; set; }
        public string StatusApproval { get; set; }
        public DateTime DateApproval { get; set; }
    }
}
