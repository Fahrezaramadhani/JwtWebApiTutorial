using AutoMapper;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Schedule;
using JwtWebApiTutorial.Responses.Schedules;

namespace JwtWebApiTutorial.Mappers
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<PostScheduleRequest, AttendanceSchedule>();
            CreateMap<AttendanceSchedule, GetScheduleResponse>()
                .ForMember(dest =>
                    dest.ScheduleId,
                    opt => opt.MapFrom(src => src.Id));
            CreateMap<PostScheduleRequest, AttendanceSchedule>();
            CreateMap<AttendanceSchedule, GetAllScheduleResponse>()
                .ForMember(dest =>
                    dest.ScheduleId,
                    opt => opt.MapFrom(src => src.Id));
            CreateMap<PutScheduleRequest, AttendanceSchedule>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.ScheduleId));
        }
    }
}
