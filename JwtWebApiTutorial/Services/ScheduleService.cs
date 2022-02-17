using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Schedule;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Services.Interface;

namespace JwtWebApiTutorial.Services
{
    public class ScheduleService : IScheduleService
    {
        public static Schedule schedule = new Schedule();
        private readonly DataContext _dbContext;

        public ScheduleService(DataContext _context)
        {
            _dbContext = _context;
        }

        public async Task<Response> Add(PostScheduleRequest postScheduleRequest)
        {
            var _lastSchedule = _dbContext.Schedules.OrderByDescending(x => x.Id).FirstOrDefault();

            //Check either the table schedule is null or not
            if (_lastSchedule != null)
            {
                schedule.Id = _lastSchedule.Id + 1;
            }

            //Add the value in request to schedule object
            schedule.Name = postScheduleRequest.Name;
            schedule.StartCheckinAt = postScheduleRequest.StartCheckoutAt;
            schedule.EndCheckinAt = postScheduleRequest.EndCheckinAt;
            schedule.StartCheckoutAt = postScheduleRequest.StartCheckoutAt;
            schedule.EndCheckoutAt = postScheduleRequest.EndCheckoutAt;
            
            //Add schedule object to database
            _dbContext.Schedules.Add(schedule);
            await _dbContext.SaveChangesAsync();

            return new Response
            {
                Message = "OK",
                Status = 200
            };
        }
    }
}
