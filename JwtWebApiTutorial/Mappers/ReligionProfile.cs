using AutoMapper;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Religion;
using JwtWebApiTutorial.Responses.Religion;

namespace JwtWebApiTutorial.Mappers
{
    public class ReligionProfile : Profile
    {
        public ReligionProfile()
        {
            CreateMap<Religion, GetReligionResponse>()
                .ForMember(dest =>
                    dest.ReligionId,
                    opt => opt.MapFrom(src => src.Id));

            CreateMap<PutReligionRequest, Religion>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.ReligionId));
        }
    }
}
