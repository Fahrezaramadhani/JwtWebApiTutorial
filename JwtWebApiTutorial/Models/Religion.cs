using System.ComponentModel.DataAnnotations.Schema;

namespace JwtWebApiTutorial.Models
{
    public class Religion
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string ReligionName { get; set; }

        // 1 to many relationship
        public List<User> Users { get; set; }

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
