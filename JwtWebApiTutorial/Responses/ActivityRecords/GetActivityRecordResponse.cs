namespace JwtWebApiTutorial.Responses.ActivityRecords
{
    public class GetActivityRecordResponse
    {
        public int Id { get; set; }
        public int WhatTimeIs { get; set; }
        public DateTime Date { get; set; }
        public string PhotoName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
    }
}
