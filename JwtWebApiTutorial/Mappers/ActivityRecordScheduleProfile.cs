using AutoMapper;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.ActivityRecordSchedule;
using JwtWebApiTutorial.Responses.ActivityRecordSchedules;

namespace JwtWebApiTutorial.Mappers
{
    public class ActivityRecordScheduleProfile : Profile
    {
        public ActivityRecordScheduleProfile()
        {
            CreateMap<PostActivityRecordScheduleRequest, ActivityRecordSchedule>();
            CreateMap<ActivityRecordSchedule, GetActivityRecordScheduleResponse>();
        }
    }
}
