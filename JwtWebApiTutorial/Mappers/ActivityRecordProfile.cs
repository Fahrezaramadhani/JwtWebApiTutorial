using AutoMapper;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.ActivityRecord;
using JwtWebApiTutorial.Responses.ActivityRecords;

namespace JwtWebApiTutorial.Mappers
{
    public class ActivityRecordProfile : Profile
    {
        public ActivityRecordProfile()
        {
            CreateMap<PostActivityRecordRequest, ActivityRecord>();
            CreateMap<ActivityRecord, GetActivityRecordResponse>();
            CreateMap<ActivityRecord, GetAllActivityRecordResponse>();
        }
    }
}
