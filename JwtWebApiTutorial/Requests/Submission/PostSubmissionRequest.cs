namespace JwtWebApiTutorial.Requests.Submission
{
    public class PostSubmissionRequest
    {
        public int UserId { get; set; }
        public int OvertimeId { get; set; } //hanya digunakan untuk pengajuan after overtime
        public DateTime DateSubmit { get; set; }
        public DateTime DatePerform { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string SubmissionType { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
    }
}
