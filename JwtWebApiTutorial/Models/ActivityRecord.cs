using System.ComponentModel.DataAnnotations.Schema;

namespace JwtWebApiTutorial.Models
{
    public class ActivityRecord
    {
        public int Id { get; set; }
        public int WhatTimeIs { get; set; }
        public DateTime Date { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string PhotoName { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Location { get; set; }

        //Foreign key for ActivityRecordSchedule
        public ActivityRecordSchedule ActivityRecordSchedule { get; set; }
        public int ActivityRecordScheduleId { get; set; }

        //Foreign key for User
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
