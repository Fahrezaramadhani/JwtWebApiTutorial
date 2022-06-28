using System.ComponentModel.DataAnnotations.Schema;

namespace JwtWebApiTutorial.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public Boolean IsLate { get; set; } 
        public DateTime CheckinAt { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string LocationCheckin { get; set; } = string.Empty;
        public string DescriptionCheckin { get; set; } = string.Empty;
        public DateTime CheckoutAt { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string LocationCheckout { get; set; } = string.Empty;
        public string DescriptionCheckout { get; set; } = string.Empty;

        [Column(TypeName = "varchar(100)")]
        public string PhotoName { get; set; } = string.Empty;

        //Foreign key for user id
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
