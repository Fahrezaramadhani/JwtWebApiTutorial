using JwtWebApiTutorial.Requests.Attendance;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Attendances;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IReportService
    {
        Task<Response<List<GetReportAttendanceResponse>>> GetReportAttendance(ReportModelRequest request);
    }
}
