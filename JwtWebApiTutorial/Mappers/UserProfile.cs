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
            CreateMap<User, PostLoginResponse>(); 

            CreateMap<User, GetUserResponse>()
                .ForMember(dest =>
                    dest.UserId,
                    opt => opt.MapFrom(src => src.Id));

            CreateMap<User, GetAllUserResponse>()
                .ForMember(dest =>
                    dest.UserId,
                    opt => opt.MapFrom(src => src.Id));

            CreateMap<PostUserRequest, User>();

            CreateMap<PutUserRequest, User>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.UserId));
        }
    }
}
