using JwtWebApiTutorial.Requests.Auth;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Auths;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IAuthService
    {
        Task<Response<PostLoginResponse>> Login(PostLoginRequest loginRequest);
        Task<Response<PostRefreshResponse>> Refresh(string refreshRequest);
    }
}
