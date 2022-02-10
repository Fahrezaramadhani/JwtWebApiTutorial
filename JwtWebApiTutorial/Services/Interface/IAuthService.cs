using JwtWebApiTutorial.Requests.Auth;
using JwtWebApiTutorial.Responses.Auth;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IAuthService
    {
        Task<PostRegisterResponse> Register(PostRegisterRequest registerRequest);
        Task<PostLoginResponse> Login(PostLoginRequest loginRequest);
        Task<PostRefreshResponse> Refresh(PostRefreshRequest refreshRequest);
    }
}
