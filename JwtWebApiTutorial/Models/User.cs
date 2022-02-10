﻿namespace JwtWebApiTutorial.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Religion { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int Current_salary { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime Join_date { get; set; }
        public DateTime End_date { get; set; }
        public string Phone_number { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string No_ktp { get; set; } = string.Empty;
        public string Npwp { get; set; } = string.Empty;
        public DateTime Date_of_birth { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Refresh_token { get; set; } = string.Empty;
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime? Deleted_at { get; set; }
        public string Created_by { get; set; } = string.Empty;
        public string Updated_by { get; set; } = string.Empty;
        public string Deleted_by { get; set; } = string.Empty;

    }
}
