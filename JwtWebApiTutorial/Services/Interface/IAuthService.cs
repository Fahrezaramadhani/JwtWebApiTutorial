using JwtWebApiTutorial.Requests.Auth;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Auths;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IAuthService
    {
        Task<Response<PostLoginResponse>> LoginMobile(PostLoginRequest loginRequest);
        Task<Response<PostLoginResponse>> LoginWeb(PostLoginRequest loginRequest);
        Task<Response<PostRefreshResponse>> Refresh(string refreshRequest);
    }
}
