using AutoMapper;
using JwtWebApiTutorial.Constants;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Attendance;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Attendances;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace JwtWebApiTutorial.Services
{
    public class ReportService : IReportService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public ReportService(DataContext _context, IMapper mapper)
        {
            _dbContext = _context;
            _mapper = mapper;
        }

        
        public async Task<Response<List<GetReportAttendanceResponse>>> GetReportAttendance(ReportModelRequest request)
        {
            var DateTimeRequest = new DateTime(request.number_year, request.number_month, 1);
            var _users = await _dbContext.Users.Include(u => u.Position).Where(u => (u.Role.Equals("HR") || u.Role.Equals("Employee"))
                                                                                && u.JoinDate <= DateTimeRequest).ToListAsync();


            List<GetReportAttendanceResponse> response = new List<GetReportAttendanceResponse>();
            string[] DateCompany = CompanyConstant.CompanyYear.Split("-");
            var CompanyYear = new DateOnly(int.Parse(DateCompany[0]), int.Parse(DateCompany[1]), DateTime.Now.Day);
            var DateRequest = new DateOnly(request.number_year, request.number_month, DateTime.Now.Day);
            if (DateOnly.FromDateTime(DateTime.Now) >= DateRequest && CompanyYear <= DateRequest)
            {
                if (_users != null)
                {
                    _users.ForEach(delegate (User user)
                    {
                        int TotalHoursAttendance = 0;
                        int TotalDaysAlpha = 0;
                        var _attendances = _dbContext.Attendances.Where(s => s.UserId == user.Id
                                                                            && s.CheckinAt.Month.Equals(request.number_month)
                                                                            && s.CheckinAt.Year.Equals(request.number_year)).ToList();

                        int TotalHoursPermit = 0;
                        var _submissionPermit = _dbContext.Submissions.Include(a => a.SubmissionAttribute).Where(s => s.SubmissionAttribute.UserId == user.Id
                                                                                && s.DatePerform.Month.Equals(request.number_month)
                                                                                && s.SubmissionType.Equals("Permit")
                                                                                && s.SubmissionAttribute.SubmissionStatus.Equals("Approved")
                                                                                && s.DatePerform.Year.Equals(request.number_year)).ToList();

                        int TotalHoursAfterOvertime = 0;
                        var _submissionAfterOvertimes = _dbContext.Submissions.Include(a => a.SubmissionAttribute).Where(s => s.SubmissionAttribute.UserId == user.Id
                                                                                && s.DatePerform.Month.Equals(request.number_month)
                                                                                && s.SubmissionType.Equals("AfterOvertime")
                                                                                && s.SubmissionAttribute.SubmissionStatus.Equals("Approved")
                                                                                && s.DatePerform.Year.Equals(request.number_year)).ToList();
                        int TotalDaysLeave = 0;
                        int TotalDaysSickLeave = 0;
                        var _submissionLeaves = _dbContext.SubmissionLeaves.Include(a => a.SubmissionAttribute).Where(s => s.SubmissionAttribute.UserId == user.Id
                                                                        && s.DateStart.Month.Equals(request.number_month)
                                                                        && s.SubmissionAttribute.SubmissionStatus.Equals("Approved")
                                                                        && s.DateStart.Year.Equals(request.number_year)).ToList();

                        var firstDayOfMonth = new DateOnly(request.number_year, request.number_month, 1);
                        var JoinDate = DateOnly.FromDateTime(user.JoinDate);
                        if (JoinDate < firstDayOfMonth)
                        {
                            int endDayOfMonth = 0;
                            if (DateTime.Now.Year.Equals(request.number_year) && DateTime.Now.Month.Equals(request.number_month))
                            {
                                endDayOfMonth = DateTime.Now.Day;
                            }
                            else
                            {
                                endDayOfMonth = DateTime.DaysInMonth(request.number_year, request.number_month);
                            }
                            for (int i = 1; i <= endDayOfMonth; i++)
                            {
                                var CurrentDate = new DateOnly(request.number_year, request.number_month, i);
                                if (CurrentDate.DayOfWeek != DayOfWeek.Sunday && CurrentDate.DayOfWeek != DayOfWeek.Saturday)
                                {
                                    var submissionLeave = _submissionLeaves.Where(u => DateOnly.FromDateTime(u.DateStart) <= CurrentDate && DateOnly.FromDateTime(u.DateEnd) >= CurrentDate).FirstOrDefault();
                                    var attendance = _attendances.Where(u => DateOnly.FromDateTime(u.CheckinAt).Equals(CurrentDate)).FirstOrDefault();
                                    if (submissionLeave != null)
                                    {
                                        if (submissionLeave.LeaveType.Equals("Sick"))
                                        {
                                            TotalDaysSickLeave = TotalDaysSickLeave + 1;
                                        }
                                        else
                                        {
                                            TotalDaysLeave = TotalDaysLeave + 1;
                                        }
                                    }
                                    else if (attendance != null)
                                    {
                                        var TimeCheckin = TimeOnly.FromDateTime(attendance.CheckinAt);
                                        if (DateOnly.FromDateTime(attendance.CheckoutAt).Equals(new DateOnly(0001, 01, 01)))
                                        {
                                            var TImeCheckout = new TimeOnly(17, 0);
                                            TotalHoursAttendance = TotalHoursAttendance + (TImeCheckout - TimeCheckin).Hours;
                                        }
                                        else
                                        {
                                            var TImeCheckout = TimeOnly.FromDateTime(attendance.CheckoutAt);
                                            TotalHoursAttendance = TotalHoursAttendance + (TImeCheckout - TimeCheckin).Hours;
                                        }
                                    }
                                    else
                                    {
                                        TotalDaysAlpha = TotalDaysAlpha + 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            int endDayOfMonth = 0;
                            if (DateTime.Now.Year.Equals(request.number_year) && DateTime.Now.Month.Equals(request.number_month))
                            {
                                endDayOfMonth = DateTime.Now.Day;
                            }
                            else
                            {
                                endDayOfMonth = DateTime.DaysInMonth(request.number_year, request.number_month);
                            }
                            for (int i = JoinDate.Day; i <= endDayOfMonth; i++)
                            {
                                var CurrentDate = new DateOnly(request.number_year, request.number_month, i);
                                if (CurrentDate.DayOfWeek != DayOfWeek.Sunday && CurrentDate.DayOfWeek != DayOfWeek.Saturday)
                                {
                                    var submissionLeave = _submissionLeaves.Where(u => DateOnly.FromDateTime(u.DateStart) <= CurrentDate && DateOnly.FromDateTime(u.DateEnd) >= CurrentDate).FirstOrDefault();
                                    var attendance = _attendances.Where(u => DateOnly.FromDateTime(u.CheckinAt).Equals(CurrentDate)).FirstOrDefault();
                                    if (submissionLeave != null)
                                    {
                                        if (submissionLeave.LeaveType.Equals("Sick"))
                                        {
                                            TotalDaysSickLeave = TotalDaysSickLeave + 1;
                                        }
                                        else
                                        {
                                            TotalDaysLeave = TotalDaysLeave + 1;
                                        }
                                    }
                                    else if (attendance != null)
                                    {
                                        var TimeCheckin = TimeOnly.FromDateTime(attendance.CheckinAt);
                                        if (DateOnly.FromDateTime(attendance.CheckoutAt).Equals(new DateOnly(0001, 01, 01)))
                                        {
                                            var TImeCheckout = new TimeOnly(17, 0);
                                            TotalHoursAttendance = TotalHoursAttendance + (TImeCheckout - TimeCheckin).Hours;
                                        }
                                        else
                                        {
                                            var TImeCheckout = TimeOnly.FromDateTime(attendance.CheckoutAt);
                                            TotalHoursAttendance = TotalHoursAttendance + (TImeCheckout - TimeCheckin).Hours;
                                        }
                                    }
                                    else
                                    {
                                        TotalDaysAlpha = TotalDaysAlpha + 1;
                                    }
                                }
                            }
                        }

                        _submissionPermit.ForEach(delegate (Submission submission)
                        {
                            string[] startTime = submission.StartTime.Split(":");
                            string[] endTime = submission.EndTime.Split(":");
                            var TimeStart = new TimeOnly(int.Parse(startTime[0]), int.Parse(startTime[1]));
                            var TimeEnd = new TimeOnly(int.Parse(endTime[0]), int.Parse(endTime[1]));
                            TotalHoursPermit = TotalHoursPermit + (TimeEnd - TimeStart).Hours;
                        });

                        _submissionAfterOvertimes.ForEach(delegate (Submission submission)
                        {
                            string[] startTime = submission.StartTime.Split(":");
                            string[] endTime = submission.EndTime.Split(":");
                            var TimeStart = new TimeOnly(int.Parse(startTime[0]), int.Parse(startTime[1]));
                            var TimeEnd = new TimeOnly(int.Parse(endTime[0]), int.Parse(endTime[1]));
                            TotalHoursAfterOvertime = TotalHoursAfterOvertime + (TimeEnd - TimeStart).Hours;
                        });

                        var UserReport = new GetReportAttendanceResponse();
                        UserReport.UserId = user.Id;
                        UserReport.UserName = user.Name;
                        UserReport.Position = user.Position.PositionName;
                        UserReport.TotalHoursPermit = TotalHoursPermit;
                        UserReport.TotalHoursAfterOvertime = TotalHoursAfterOvertime;
                        UserReport.TotalHoursAttendance = TotalHoursAttendance;
                        UserReport.TotalDaysLeave = TotalDaysLeave;
                        UserReport.TotalDaysSickLeave = TotalDaysSickLeave;
                        UserReport.TotalDaysAlpha = TotalDaysAlpha;

                        response.Add(UserReport);
                    });
                }
                else
                {
                    return new Response<List<GetReportAttendanceResponse>>
                    {
                        Message = "Not found",
                        Status = 404,
                        Data = response
                    };
                }
            }
            
            return new Response<List<GetReportAttendanceResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = response
            };
        }
        
    }
}
