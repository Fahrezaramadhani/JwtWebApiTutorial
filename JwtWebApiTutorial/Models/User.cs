using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtWebApiTutorial.Models
{
    public class User
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; } = string.Empty;

        public Religion Religion { get; set; }
        public int ReligionId { get; set; }

        public Position Position { get; set; }
        public int PositionId { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Gender { get; set; } = string.Empty;

        [Column(TypeName = "varchar(25)")]
        public string Status { get; set; } = string.Empty;

        [Column(TypeName = "Date")]
        public DateTime JoinDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? EndDate { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "varchar(150)")]
        public string Address { get; set; } = string.Empty;

        [Column(TypeName = "varchar(25)")]
        public string City { get; set; } = string.Empty;

        [Column(TypeName = "varchar(20)")]
        public string NoKTP { get; set; } = string.Empty;

        [Column(TypeName = "varchar(20)")]
        public string? NPWP { get; set; } = string.Empty;

        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Role { get; set; } = string.Empty;

        [Column(TypeName = "varchar(250)")]
        public string Password { get; set; } = string.Empty;

        [Column(TypeName = "varchar(100)")]
        public string PhotoName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(250)")]
        public string RefreshToken { get; set; } = string.Empty;
        public int SuperiorId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string CreatedBy { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string UpdatedBy { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string DeletedBy { get; set; } = string.Empty;


        // 1 to many relationship
        public List<Attendance> Attendances { get; set; }
        public List<ActivityRecord> ActivityRecords { get; set; }
        public List<SubmissionAttribute> SubmissionAttributes { get; set; }
    }
}
