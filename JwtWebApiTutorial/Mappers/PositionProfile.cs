using AutoMapper;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Position;
using JwtWebApiTutorial.Responses.Position;

namespace JwtWebApiTutorial.Mappers
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<Position, GetPositionResponse>()
                .ForMember(dest =>
                    dest.PositionId,
                    opt => opt.MapFrom(src => src.Id));

            CreateMap<PutPositionRequest, Position>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.PositionId));
        }
    }
}
