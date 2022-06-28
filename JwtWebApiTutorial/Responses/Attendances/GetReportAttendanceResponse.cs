namespace JwtWebApiTutorial.Responses.Attendances
{
    public class GetReportAttendanceResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Position { get; set; }
        public int TotalHoursAttendance { get; set; }
        public int TotalHoursPermit { get; set; }
        public int TotalHoursAfterOvertime { get; set; }
        public int TotalDaysLeave { get; set; }
        public int TotalDaysSickLeave { get; set; }
        public int TotalDaysAlpha { get; set; }
    }
}
