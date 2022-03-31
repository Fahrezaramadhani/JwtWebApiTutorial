using JwtWebApiTutorial.Requests.ActivityRecord;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.ActivityRecords;
using Sieve.Models;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IActivityRecordService
    {
        Task<Response<string>> Add(PostActivityRecordRequest postActivityRecordRequest);
        Task<Response<GetActivityRecordResponse>> Get(int id);
        Task<Response<PaginatedResponse<GetAllActivityRecordResponse>>> GetPagedActivityRecordList(SieveModel sieveModel);
        Task<Response<IEnumerable<GetAllActivityRecordResponse>>> GetActivityRecordList(SieveModel sieveModel);
    }
}
