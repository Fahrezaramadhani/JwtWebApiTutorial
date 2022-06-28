﻿namespace JwtWebApiTutorial.Requests.SubmissionLeave
{
    public class PutSubmissionLeaveRequest
    {
        public int? SubmissionLeaveId { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
    }
}
