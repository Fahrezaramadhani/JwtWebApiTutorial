using JwtWebApiTutorial.Requests.ApplicationSetting;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.ApplicationSettings;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IApplicationSettingService
    {
        Task<Response<string>> Add(PostApplicationSettingRequest postApplicationSettingRequest);
        Task<Response<GetApplicationSettingResponse>> Get(int id);
    }
}
