using System.ComponentModel.DataAnnotations.Schema;

namespace JwtWebApiTutorial.Models
{
    public class SubmissionLeave
    {
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateStart { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateEnd { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string LeaveType { get; set; }

        //Foreign Key SubmissionAttribute
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
