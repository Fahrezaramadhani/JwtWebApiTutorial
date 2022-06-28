using JwtWebApiTutorial.Requests.Position;
using JwtWebApiTutorial.Requests.User;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Position;
using Sieve.Models;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IPositionService
    {
        Task<Response<string>> AddPosition(string request);
        Task<Response<GetPositionResponse>> GetPosition(int id);
        Task<Response<List<GetPositionResponse>>> GetPositionList();
        Task<Response<string>> UpdatePosition(PutPositionRequest request);
        Task<Response<string>> DeletePosition(int id);
    }
}
