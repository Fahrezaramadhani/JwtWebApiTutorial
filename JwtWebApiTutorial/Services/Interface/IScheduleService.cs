using JwtWebApiTutorial.Requests.Schedule;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Schedules;
using Sieve.Models;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IScheduleService
    {
        Task<Response<string>> Add(PostScheduleRequest postScheduleRequest);
        Task<Response<GetScheduleResponse>> Get(int id);
        Task<Response<PaginatedResponse<GetAllScheduleResponse>>> GetPagedScheduleList(SieveModel sieveModel);
        Task<Response<IEnumerable<GetAllScheduleResponse>>> GetScheduleList(SieveModel sieveModel);
        Task<Response<string>> Update(PutScheduleRequest putScheduleRequest);
        Task<Response<string>> Delete(int id);
    }
}
