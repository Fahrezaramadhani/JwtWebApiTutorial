using JwtWebApiTutorial.Requests.ActivityRecordSchedule;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.ActivityRecordSchedules;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IActivityRecordScheduleService
    {
        Task<Response<string>> Add(PostActivityRecordScheduleRequest postActivityRecordScheduleRequest);
        Task<Response<GetActivityRecordScheduleResponse>> Get(int id);
    }
}
