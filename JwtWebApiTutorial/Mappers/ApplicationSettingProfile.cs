using AutoMapper;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.ApplicationSetting;
using JwtWebApiTutorial.Responses.ApplicationSettings;

namespace JwtWebApiTutorial.Mappers
{
    public class ApplicationSettingProfile : Profile
    {
        public ApplicationSettingProfile()
        {
            CreateMap<PostApplicationSettingRequest, ApplicationSetting>();
            CreateMap<ApplicationSetting, GetApplicationSettingResponse>();
        }
    }
}
