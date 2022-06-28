using AutoMapper;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.User;
using JwtWebApiTutorial.Responses.Auths;
using JwtWebApiTutorial.Responses.Users;

namespace JwtWebApiTutorial.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, PostLoginResponse>()
                .ForMember(dest =>
                    dest.Position,
                    opt => opt.MapFrom(src => src.Position.PositionName)); 

            CreateMap<User, GetUserResponse>()
                .ForMember(dest =>
                    dest.UserId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest =>
                    dest.Position,
                    opt => opt.MapFrom(src => src.Position.PositionName))
                .ForMember(dest =>
                    dest.Religion,
                    opt => opt.MapFrom(src => src.Religion.ReligionName));

            CreateMap<User, GetAllUserResponse>()
                .ForMember(dest =>
                    dest.UserId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest =>
                    dest.Position,
                    opt => opt.MapFrom(src => src.Position.PositionName))
                .ForMember(dest =>
                    dest.Religion,
                    opt => opt.MapFrom(src => src.Religion.ReligionName));

            CreateMap<PostUserRequest, User>();

            CreateMap<PutUserRequest, User>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.UserId));
        }
    }
}
