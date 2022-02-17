using JwtWebApiTutorial.Requests.Schedule;
using JwtWebApiTutorial.Responses;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IScheduleService
    {
        Task<Response> Add(PostScheduleRequest postScheduleRequest);
    }
}
