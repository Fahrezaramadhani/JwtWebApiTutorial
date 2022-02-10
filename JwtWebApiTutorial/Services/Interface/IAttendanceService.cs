using JwtWebApiTutorial.Requests.Attendance;
using JwtWebApiTutorial.Responses.Attendance;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IAttendanceService
    {
        Task<PostCheckinResponse> Checkin(PostCheckinRequest checkinRequest);
    }
}
