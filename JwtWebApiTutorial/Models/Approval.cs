using System.ComponentModel.DataAnnotations.Schema;

namespace JwtWebApiTutorial.Models
{
    public class Approval
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string StatusApproval { get; set; }
        public DateTime DateApproval { get; set; }

        //Foreign Key attribute
        public SubmissionAttribute SubmissionAttribute { get; set; }
        public int SubmissionAttributeId { get; set; }

        //Foreign Key User
        public User User { get; set; }
        public int UserId { get; set; }

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
