using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Attendance;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Services.Interface;

namespace JwtWebApiTutorial.Services
{
    public class AttendanceService : IAttendanceService
    {
        public static Attendance attendance = new Attendance();
        private readonly DataContext _dbContext;

        public AttendanceService(DataContext _context)
        {
            _dbContext = _context;
        }

        public async Task<Response> CheckinOnline(PostCheckinOnlineRequest postCheckinOnlineRequest)
        {
            var LastAttendance = _dbContext.Attendances.OrderByDescending(x => x.Id).FirstOrDefault();
            var schedule = _dbContext.Schedules.FirstOrDefault(x => x.Id == postCheckinOnlineRequest.ScheduleId);

            //Check either the table user is null or not
            if (LastAttendance != null)
            {
                attendance.Id = LastAttendance.Id + 1;
            }

            //Check is late
            if (postCheckinOnlineRequest.Checkin > schedule.EndCheckinAt)
            {
                attendance.IsLate = "true";
            }
            else
            {
                attendance.IsLate = "false";
            }

            //Create Attendance
            attendance.UserId = postCheckinOnlineRequest.UserId;
            attendance.ScheduleId = postCheckinOnlineRequest.ScheduleId;
            attendance.CheckinAt = postCheckinOnlineRequest.Checkin;
            attendance.LocationCheckin = postCheckinOnlineRequest.Location;
            attendance.DescriptionCheckin = postCheckinOnlineRequest.Description;
            attendance.PhotoName = postCheckinOnlineRequest.PhotoName;

            //Save Attendance to database
            _dbContext.Attendances.Add(attendance);
            await _dbContext.SaveChangesAsync();

            return new Response
            {
                Message = "OK",
                Status = 200
            };
        }

        public async Task<Response> CheckoutOnline(PostCheckoutOnlineRequest postCheckoutOnlineRequest)
        {
            var LastAttendance = _dbContext.Attendances.FirstOrDefault(x => x.UserId == postCheckoutOnlineRequest.UserId);

            //Check either the table user is null or not
            if (LastAttendance != null)
            {
                return new Response
                {
                    Message = "Not found",
                    Status = 404
                };
            }

            LastAttendance.CheckoutAt = postCheckoutOnlineRequest.Checkout;
            LastAttendance.LocationCheckout = postCheckoutOnlineRequest.Location;
            LastAttendance.DescriptionCheckout = postCheckoutOnlineRequest.Description;

            await _dbContext.SaveChangesAsync();

            return new Response
            {
                Message = "OK",
                Status = 200
            };
        }

        public async Task<Response> CheckoutOffline(PostCheckinOfflineRequest postCheckinOfflineRequest)
        {
            var LastAttendance = _dbContext.Attendances.OrderByDescending(x => x.Id).FirstOrDefault();
            var schedule = _dbContext.Schedules.FirstOrDefault(x => x.Id == postCheckinOfflineRequest.ScheduleId);

            //Check either the table user is null or not
            if (LastAttendance != null)
            {
                attendance.Id = LastAttendance.Id + 1;
            }

            //Check is late
            if (postCheckinOfflineRequest.Checkin > schedule.EndCheckinAt)
            {
                attendance.IsLate = "true";
            }
            else
            {
                attendance.IsLate = "false";
            }

            //Create Attendance
            attendance.UserId = postCheckinOfflineRequest.UserId;
            attendance.ScheduleId = postCheckinOfflineRequest.ScheduleId;
            attendance.CheckinAt = postCheckinOfflineRequest.Checkin;
            attendance.LocationCheckin = postCheckinOfflineRequest.Location;
            attendance.DescriptionCheckin = postCheckinOfflineRequest.Description;

            //Save Attendance to database
            _dbContext.Attendances.Add(attendance);
            await _dbContext.SaveChangesAsync();

            return new Response
            {
                Message = "OK",
                Status = 200
            };
        }

        public async Task<Response> CheckoutOffline(PostCheckoutOfflineRequest postCheckoutOfflineRequest)
        {
            var LastAttendance = _dbContext.Attendances.FirstOrDefault(x => x.UserId == postCheckoutOfflineRequest.UserId);

            //Check either the table user is null or not
            if (LastAttendance != null)
            {
                return new Response
                {
                    Message = "Not found",
                    Status = 404
                };
            }

            LastAttendance.CheckoutAt = postCheckoutOfflineRequest.Checkout;
            LastAttendance.LocationCheckout = postCheckoutOfflineRequest.Location;
            LastAttendance.DescriptionCheckout = postCheckoutOfflineRequest.Description;

            await _dbContext.SaveChangesAsync();

            return new Response
            {
                Message = "OK",
                Status = 200
            };
        }
    }
}
