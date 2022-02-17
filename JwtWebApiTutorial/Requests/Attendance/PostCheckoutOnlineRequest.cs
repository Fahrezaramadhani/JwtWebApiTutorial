﻿namespace JwtWebApiTutorial.Requests.Attendance
{
    public class PostCheckoutOnlineRequest
    {
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
        public string Location { get; set; }
        public DateTime Checkout { get; set; }
        public string Description { get; set; }
    }
}
