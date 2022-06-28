namespace JwtWebApiTutorial.Requests.Submission
{
    public class PutSubmissionRequest
    {
        public int SubmissionId { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime DatePerform { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
    }
}
