using AutoMapper;
using JwtWebApiTutorial.Constants;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Helpers;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Attendance;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Attendances;
using JwtWebApiTutorial.Services.Interface;
using JwtWebApiTutorial.Sieve;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;

namespace JwtWebApiTutorial.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationSieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;
        private readonly DataContext _dbContext;

        public AttendanceService(DataContext _context, ApplicationSieveProcessor sieve, IMapper mapper)
        {
            _dbContext = _context;
            _sieveProcessor = sieve;
            _mapper = mapper;
        }

        public async Task<Response<string>> CheckinOnline(PostCheckinOnlineRequest postCheckinOnlineRequest)
        {
            Attendance attendance = new Attendance();
            var LastAttendance = _dbContext.Attendances.OrderByDescending(x => x.Id).FirstOrDefault();
            var schedule = _dbContext.AttendanceSchedules.FirstOrDefault(x => x.Id == postCheckinOnlineRequest.ScheduleId);

            //Check either the table user is null or not
            if (LastAttendance != null)
            {
                attendance.Id = LastAttendance.Id + 1;
            }

            if (schedule == null)
            {
                return new Response<string>
                {
                    Message = "Not Found",
                    Status = 404,
                    Data = ""
                };
            }

            //Get TimeOnly Data
            TimeOnly EndCheckinAt = TimeOnly.FromDateTime(schedule.EndCheckinAt);
            TimeOnly Checkin = TimeOnly.FromDateTime(postCheckinOnlineRequest.CheckinTime);

            //Check is late
            if (Checkin > EndCheckinAt)
            {
                attendance.IsLate = true;
            }
            else
            {
                attendance.IsLate = false;
            }

            //Create Attendance
            attendance.UserId = postCheckinOnlineRequest.UserId;
            attendance.ScheduleId = postCheckinOnlineRequest.ScheduleId;
            attendance.CheckinAt = postCheckinOnlineRequest.CheckinTime;
            attendance.LocationCheckin = postCheckinOnlineRequest.Location;
            attendance.DescriptionCheckin = postCheckinOnlineRequest.Description;
            attendance.PhotoName = postCheckinOnlineRequest.PhotoName;

            if (!string.IsNullOrWhiteSpace(attendance.PhotoName) && UploadImageHelper.IsBase64(attendance.PhotoName))
            {
                attendance.PhotoName = UploadImageHelper.UploadBase64File($"{DateTime.Now:yyyyMMddhhmmss}{attendance.UserId}", attendance.PhotoName, "Images/Attendance");
            }

            if (attendance.PhotoName == string.Empty || attendance.PhotoName == null)
            {
                attendance.PhotoName = ImageConstant.DEFAULTIMAGE;
                attendance.PhotoName = UploadImageHelper.UploadBase64File("DefaultImage", attendance.PhotoName, "Images/Attendance");
            }

            //Save Attendance to database
            _dbContext.Attendances.Add(attendance);
            await _dbContext.SaveChangesAsync();

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<string>> CheckoutOnline(PostCheckoutOnlineRequest postCheckoutOnlineRequest)
        {
            var LastAttendance = _dbContext.Attendances.OrderByDescending(x => x.Id).FirstOrDefault(x => x.UserId == postCheckoutOnlineRequest.UserId);

            DateOnly checkoutTime = DateOnly.FromDateTime(postCheckoutOnlineRequest.CheckoutTime);
            DateOnly checkinTime = DateOnly.FromDateTime(LastAttendance.CheckinAt);
            //Check either the table user is null or not
            if (LastAttendance == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }
            else if (checkinTime != checkoutTime)
            {
                return new Response<string>
                {
                    Message = "Not found checkin time",
                    Status = 404,
                    Data = ""
                };
            }

            LastAttendance.CheckoutAt = postCheckoutOnlineRequest.CheckoutTime;
            LastAttendance.LocationCheckout = postCheckoutOnlineRequest.Location;
            LastAttendance.DescriptionCheckout = postCheckoutOnlineRequest.Description;

            await _dbContext.SaveChangesAsync();

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<string>> CheckinOffline(PostCheckinOfflineRequest postCheckinOfflineRequest)
        {
            Attendance attendance = new Attendance();
            var LastAttendance = _dbContext.Attendances.OrderByDescending(x => x.Id).FirstOrDefault();
            var schedule = _dbContext.AttendanceSchedules.FirstOrDefault(x => x.Id == postCheckinOfflineRequest.ScheduleId);

            //Check either the table user is null or not
            if (LastAttendance != null)
            {
                attendance.Id = LastAttendance.Id + 1;
            }

            //Get TimeOnly Data
            TimeOnly EndCheckinAt = TimeOnly.FromDateTime(schedule.EndCheckinAt);
            TimeOnly Checkin = TimeOnly.FromDateTime(postCheckinOfflineRequest.CheckinTime);

            //Check is late
            if (Checkin > EndCheckinAt)
            {
                attendance.IsLate = true;
            }
            else
            {
                attendance.IsLate = false;
            }

            //Create Attendance
            attendance.UserId = postCheckinOfflineRequest.UserId;
            attendance.ScheduleId = postCheckinOfflineRequest.ScheduleId;
            attendance.CheckinAt = postCheckinOfflineRequest.CheckinTime;
            attendance.LocationCheckin = postCheckinOfflineRequest.Location;
            attendance.DescriptionCheckin = postCheckinOfflineRequest.Description;

            //Save Attendance to database
            _dbContext.Attendances.Add(attendance);
            await _dbContext.SaveChangesAsync();

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<string>> CheckoutOffline(PostCheckoutOfflineRequest postCheckoutOfflineRequest)
        {
            var LastAttendance = _dbContext.Attendances.OrderByDescending(x => x.Id).FirstOrDefault(x => x.UserId == postCheckoutOfflineRequest.UserId);

            DateOnly checkoutTime = DateOnly.FromDateTime(postCheckoutOfflineRequest.CheckoutTime);
            DateOnly checkinTime = DateOnly.FromDateTime(LastAttendance.CheckinAt);
            //Check either the table user is null or not
            if (LastAttendance == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }
            else if (checkinTime != checkoutTime)
            {
                return new Response<string>
                {
                    Message = "Not found checkin time",
                    Status = 404,
                    Data = ""
                };
            }

                LastAttendance.CheckoutAt = postCheckoutOfflineRequest.CheckoutTime;
            LastAttendance.LocationCheckout = postCheckoutOfflineRequest.Location;
            LastAttendance.DescriptionCheckout = postCheckoutOfflineRequest.Description;

            await _dbContext.SaveChangesAsync();

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<PaginatedResponse<GetAllAttendanceResponse>>> GetPagedAttendanceList(SieveModel sieveModel)
        {
            var attendances = _dbContext.Attendances.AsNoTracking();

            attendances = _sieveProcessor.Apply(sieveModel, attendances, applyPagination: false);

            int count = await attendances.CountAsync();

            attendances = _sieveProcessor.Apply(sieveModel, attendances, applyPagination: true);

            var result = await PaginationHelper.GetPagedResultAsync<Attendance, GetAllAttendanceResponse>(attendances, sieveModel, _mapper, count);

            return new Response<PaginatedResponse<GetAllAttendanceResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetAllAttendanceResponse>>> GetAttendanceList(SieveModel sieveModel)
        {
            var attendances = _dbContext.Attendances.AsNoTracking();

            attendances = _sieveProcessor.Apply(sieveModel, attendances, applyPagination: false);

            var result = await attendances.Select(u => _mapper.Map<GetAllAttendanceResponse>(u)).ToListAsync();

            return new Response<IEnumerable<GetAllAttendanceResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }
    }
}
