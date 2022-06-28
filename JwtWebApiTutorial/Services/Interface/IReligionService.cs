using JwtWebApiTutorial.Requests.Religion;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Religion;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IReligionService
    {
        Task<Response<string>> AddReligion(string request);
        Task<Response<GetReligionResponse>> GetReligion(int id);
        Task<Response<List<GetReligionResponse>>> GetReligionList();
        Task<Response<string>> UpdateReligion(PutReligionRequest request);
        Task<Response<string>> DeleteReligion(int id);
    }
}
