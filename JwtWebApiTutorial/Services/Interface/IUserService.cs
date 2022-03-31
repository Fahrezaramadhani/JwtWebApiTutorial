using JwtWebApiTutorial.Requests.User;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Users;
using Sieve.Models;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IUserService
    {
        Task<Response<string>> Add(PostUserRequest data);
        Task<Response<GetUserResponse>> Get(int id);
        Task<Response<PaginatedResponse<GetAllUserResponse>>> GetPagedUserList(SieveModel sieveModel);
        Task<Response<IEnumerable<GetAllUserResponse>>> GetUserList(SieveModel sieveModel);
        Task<Response<string>> Update(PutUserRequest putUserRequest);
        Task<Response<string>> Delete(int id);
    }
}
