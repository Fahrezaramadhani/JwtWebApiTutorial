using AutoMapper;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.ActivityRecordSchedule;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.ActivityRecordSchedules;
using JwtWebApiTutorial.Services.Interface;
using JwtWebApiTutorial.Sieve;

namespace JwtWebApiTutorial.Services
{
    public class ActivityRecordScheduleService : IActivityRecordScheduleService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ApplicationSieveProcessor _sieveProcessor;

        public ActivityRecordScheduleService(DataContext _context, IMapper mapper, ApplicationSieveProcessor sieveProcessor)
        {
            _dbContext = _context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Response<string>> Add(PostActivityRecordScheduleRequest postActivityRecordScheduleRequest)
        {
            var schedule = _mapper.Map<ActivityRecordSchedule>(postActivityRecordScheduleRequest);

            if (!_dbContext.ActivityRecordSchedules.Any(x => x.Name == schedule.Name))
            {
                _dbContext.ActivityRecordSchedules.Add(schedule);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return new Response<string>
                {
                    Message = "Conflict",
                    Status = 409,
                    Data = ""
                };
            }

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<GetActivityRecordScheduleResponse>> Get(int id)
        {
            var _schedule = _dbContext.AttendanceSchedules.FirstOrDefault(x => x.Id == id);
            GetActivityRecordScheduleResponse response = new GetActivityRecordScheduleResponse();

            //Check either the table schedule is null or not
            if (_schedule == null)
            {
                return new Response<GetActivityRecordScheduleResponse>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = response
                };
            }

            response = _mapper.Map<GetActivityRecordScheduleResponse>(_schedule);

            return new Response<GetActivityRecordScheduleResponse>
            {
                Message = "OK",
                Status = 200,
                Data = response
            };
        }
    }
}
