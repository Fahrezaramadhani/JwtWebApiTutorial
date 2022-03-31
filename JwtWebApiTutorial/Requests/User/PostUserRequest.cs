namespace JwtWebApiTutorial.Requests.User
{
    public class PostUserRequest
    {
        public int ScheduleId { get; set; }
        public int SuperiorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Religion { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int CurrentSalary { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string NoKtp { get; set; } = string.Empty;
        public string Npwp { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhotoName { get; set; } = string.Empty;
    }
}
