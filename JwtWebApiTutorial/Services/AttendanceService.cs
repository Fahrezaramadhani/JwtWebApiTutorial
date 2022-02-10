using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Requests.Attendance;
using JwtWebApiTutorial.Responses.Attendance;
using JwtWebApiTutorial.Services.Interface;

namespace JwtWebApiTutorial.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly DataContext _dbContext;

        public AttendanceService(DataContext _context)
        {
            _dbContext = _context;
        }

        public async Task<PostCheckinResponse> Checkin(PostCheckinRequest checkinRequest)
        {
            var LastAttendance = _dbContext.Users.OrderByDescending(user => user.Id).FirstOrDefault();

            //Check either the table user is null or not
            if (LastUser != null)
            {
                user.Id = LastUser.Id + 1;
            }
        }
    }
}
