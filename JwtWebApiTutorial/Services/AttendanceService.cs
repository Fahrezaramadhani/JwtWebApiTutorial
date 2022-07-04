using AutoMapper;
using JwtWebApiTutorial.Constants;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Helpers;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.ApplicationSetting;
using JwtWebApiTutorial.Requests.Attendance;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Attendances;
using JwtWebApiTutorial.Services.Interface;
using JwtWebApiTutorial.Sieve;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using System.Diagnostics;

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
            var LastAttendance = _dbContext.Attendances.Where(u => u.UserId == postCheckinOnlineRequest.UserId).OrderByDescending(u => u.Id).FirstOrDefault();

            /*
            if (!(LastAttendance == null))
            {
                //Get DateOnly Data
                DateOnly EndCheckinDateAt = DateOnly.FromDateTime(LastAttendance.CheckinAt);
                DateOnly CheckinDate = DateOnly.FromDateTime(postCheckinOnlineRequest.CheckinTime);

                if (EndCheckinDateAt == CheckinDate)
                {
                    return new Response<string>
                    {
                        Message = "Conflict",
                        Status = 409,
                        Data = ""
                    };
                }
            }
            */
            

            //Get TimeOnly Data
            TimeOnly EndCheckinAt = new TimeOnly(8,30);
            TimeOnly LimitCheckinAt = new TimeOnly(17, 0);
            TimeOnly Checkin = TimeOnly.FromDateTime(postCheckinOnlineRequest.CheckinTime);

            //Limit checkin
            if (Checkin > LimitCheckinAt)
            {
                return new Response<string>
                {
                    Message = "Unauthorized",
                    Status = 401,
                    Data = "",
                };
            }

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

            if (LastAttendance == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }

            DateOnly checkoutTime = DateOnly.FromDateTime(postCheckoutOnlineRequest.CheckoutTime);
            DateOnly checkinTime = DateOnly.FromDateTime(LastAttendance.CheckinAt);

            if (checkinTime != checkoutTime)
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
            var LastAttendance = _dbContext.Attendances.Where(u => u.UserId == postCheckinOfflineRequest.UserId).OrderByDescending(u => u.Id).FirstOrDefault();

            /*
            if (!(LastAttendance == null))
            {
                //Get DateOnly Data
                DateOnly EndCheckinDateAt = DateOnly.FromDateTime(LastAttendance.CheckinAt);
                DateOnly CheckinDate = DateOnly.FromDateTime(postCheckinOfflineRequest.CheckinTime);

                if (EndCheckinDateAt == CheckinDate)
                {
                    return new Response<string>
                    {
                        Message = "Conflict",
                        Status = 409,
                        Data = ""
                    };
                }
            }
            */

            //Get TimeOnly Data
            TimeOnly EndCheckinAt = new TimeOnly(8, 30);
            TimeOnly LimitCheckinAt = new TimeOnly(17, 0);
            TimeOnly Checkin = TimeOnly.FromDateTime(postCheckinOfflineRequest.CheckinTime);

            //Limit checkin
            if (Checkin > LimitCheckinAt)
            {
                return new Response<string>
                {
                    Message = "Unauthorized",
                    Status = 401,
                    Data = "",
                };
            }

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

            if (LastAttendance == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }

            DateOnly checkoutTime = DateOnly.FromDateTime(postCheckoutOfflineRequest.CheckoutTime);
            DateOnly checkinTime = DateOnly.FromDateTime(LastAttendance.CheckinAt);

            if (checkinTime != checkoutTime)
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

        public async Task<Response<string>> CheckQRCode(PostQRCodeRequest request)
        {
            Debug.WriteLine("TypeQRCode : " + request.TypeQRCode);
            Debug.WriteLine("QRCodeValue : " + request.QRCodeValue);
            if (request.TypeQRCode == "Checkin")
            {
                var activeQRCodeCheckin = await _dbContext.ApplicationSettings.FirstOrDefaultAsync(x => x.SettingName == "QRCodeCheckin");
                if (activeQRCodeCheckin == null)
                {
                    return new Response<string>
                    {
                        Message = "Not found",
                        Status = 404,
                        Data = ""
                    };
                }

                if (request.QRCodeValue == activeQRCodeCheckin.SettingValue)
                {
                    return new Response<string>
                    {
                        Message = "OK",
                        Status = 200,
                        Data = ""
                    };
                }
                else
                {
                    return new Response<string>
                    {
                        Message = "Unauthorized",
                        Status = 401,
                        Data = "",
                    };
                }
            }
            else if (request.TypeQRCode == "Checkout")
            {
                var activeQRCodeCheckout = await _dbContext.ApplicationSettings.FirstOrDefaultAsync(x => x.SettingName == "QRCodeCheckout");
                if (activeQRCodeCheckout == null)
                {
                    return new Response<string>
                    {
                        Message = "Not found",
                        Status = 404,
                        Data = ""
                    };
                }

                if (request.QRCodeValue == activeQRCodeCheckout.SettingValue)
                {
                    return new Response<string>
                    {
                        Message = "OK",
                        Status = 200,
                        Data = ""
                    };
                }
                else
                {
                    return new Response<string>
                    {
                        Message = "Unauthorized",
                        Status = 401,
                        Data = "",
                    };
                }
            }
            else
            {
                return new Response<string>
                {
                    Message = "Unauthorized",
                    Status = 401,
                    Data = "",
                };
            }
        }

        public async Task<Response<string>> CheckinStatus (DateTime request)
        {
            TimeOnly checkinTime = TimeOnly.FromDateTime(request);
            TimeOnly startCheckinAt = new TimeOnly(6, 0);
            if (checkinTime < startCheckinAt)
            {
                return new Response<string>
                {
                    Message = "Unauthorized",
                    Status = 401,
                    Data = "",
                };
            }
            else
            {
                return new Response<string>
                {
                    Message = "OK",
                    Status = 200,
                    Data = ""
                };
            }
        }
    }
}
