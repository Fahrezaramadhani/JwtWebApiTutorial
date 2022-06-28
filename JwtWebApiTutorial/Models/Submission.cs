using System.ComponentModel.DataAnnotations.Schema;

namespace JwtWebApiTutorial.Models
{
    public class Submission
    {
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DatePerform { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string StartTime { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string EndTime { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string SubmissionType { get; set; }
        public int OvertimeId { get; set; }

        //Foreign Key User
        public SubmissionAttribute SubmissionAttribute { get; set; }
        public int SubmissionAttributeId { get; set; }

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
