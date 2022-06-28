using System.ComponentModel.DataAnnotations.Schema;

namespace JwtWebApiTutorial.Models
{
    public class SubmissionAttribute
    {
        public int Id { get; set; }
        public DateTime DateSubmit { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string SubmissionStatus { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Attachment { get; set; }

        public List<Approval> Approvals { get; set; }

        //Foreign Key User
        public User User { get; set; }
        public int UserId { get; set; }

        //Foreign Key Submission
        public Submission Submission { get; set; }
        public int? SubmissionId { get; set; }

        //Foreign Key Leave
        public SubmissionLeave SubmissionLeave { get; set; }
        public int? SubmissionLeaveId { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string CreatedBy { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string UpdatedBy { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string DeletedBy { get; set; } = string.Empty;
    }
}
