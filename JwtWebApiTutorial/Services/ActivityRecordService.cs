using AutoMapper;
using JwtWebApiTutorial.Constants;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Helpers;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.ActivityRecord;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.ActivityRecords;
using JwtWebApiTutorial.Services.Interface;
using JwtWebApiTutorial.Sieve;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;

namespace JwtWebApiTutorial.Services
{
    public class ActivityRecordService : IActivityRecordService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ApplicationSieveProcessor _sieveProcessor;

        public ActivityRecordService(DataContext _context, IMapper mapper, ApplicationSieveProcessor sieveProcessor)
        {
            _dbContext = _context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Response<string>> Add(PostActivityRecordRequest postActivityRecordRequest)
        {
            var activityRecord = _mapper.Map<ActivityRecord>(postActivityRecordRequest);
            var activeSchedule = await _dbContext.ApplicationSettings.FirstOrDefaultAsync(x => x.SettingName == "ActivityRecordSchedule");
            var activityRecordSchedule = await _dbContext.ActivityRecordSchedules.FirstOrDefaultAsync(x => x.Id == int.Parse(activeSchedule.SettingValue));

            if (activityRecordSchedule == null)
            {
                return new Response<string>
                {
                    Message = "Not Found",
                    Status = 404,
                    Data = ""
                };
            }

            int numberTime = determineTime(activityRecord.Date, activityRecordSchedule);
            var findActivityRecord = await _dbContext.ActivityRecords.Where(x => x.UserId == activityRecord.UserId).OrderByDescending(x => x.Id).Take(3).ToListAsync();

            //findActivityRecord.Any()
            if (findActivityRecord.Any())
            {
                foreach (var activityRecords in findActivityRecord)
                {
                    DateOnly date = DateOnly.FromDateTime(activityRecords.Date);
                    if (activityRecords.WhatTimeIs == numberTime && date == DateOnly.FromDateTime(activityRecord.Date))
                    {
                        return new Response<string>
                        {
                            Message = "Conflict",
                            Status = 409,
                            Data = ""
                        };
                    }
                }
                /*
                return new Response<string>
                {
                    Message = "Conflict",
                    Status = 409,
                    Data = ""
                };
                */
            }

            activityRecord.ActivityRecordScheduleId = int.Parse(activeSchedule.SettingValue);
            activityRecord.WhatTimeIs = numberTime;

            if (!string.IsNullOrWhiteSpace(activityRecord.PhotoName) && UploadImageHelper.IsBase64(activityRecord.PhotoName))
            {
                activityRecord.PhotoName = UploadImageHelper.UploadBase64File($"{DateTime.Now:yyyyMMddhhmmss}{activityRecord.UserId}", activityRecord.PhotoName, "Images/ActivityRecord");
            }

            if (activityRecord.PhotoName == string.Empty || activityRecord.PhotoName == null)
            {
                activityRecord.PhotoName = ImageConstant.DEFAULTIMAGE;
                activityRecord.PhotoName = UploadImageHelper.UploadBase64File("DefaultImage", activityRecord.PhotoName, "Images/ActivityRecord");
            }

            _dbContext.ActivityRecords.Add(activityRecord);
            await _dbContext.SaveChangesAsync();

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<GetActivityRecordResponse>> Get(int id)
        {
            var _activityRecord = _dbContext.ActivityRecords.FirstOrDefault(x => x.Id == id);
            GetActivityRecordResponse getActivityRecordResponse = new GetActivityRecordResponse();

            //Check either the table schedule is null or not
            if (_activityRecord == null)
            {
                return new Response<GetActivityRecordResponse>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = getActivityRecordResponse
                };
            }

            getActivityRecordResponse = _mapper.Map<GetActivityRecordResponse>(_activityRecord);

            return new Response<GetActivityRecordResponse>
            {
                Message = "OK",
                Status = 200,
                Data = getActivityRecordResponse
            };
        }

        public async Task<Response<PaginatedResponse<GetAllActivityRecordResponse>>> GetPagedActivityRecordList(SieveModel sieveModel)
        {
            var activityRecords = _dbContext.ActivityRecords.AsNoTracking();

            activityRecords = _sieveProcessor.Apply(sieveModel, activityRecords, applyPagination: false);

            int count = await activityRecords.CountAsync();

            activityRecords = _sieveProcessor.Apply(sieveModel, activityRecords, applyPagination: true);

            var result = await PaginationHelper.GetPagedResultAsync<ActivityRecord, GetAllActivityRecordResponse>(activityRecords, sieveModel, _mapper, count);

            return new Response<PaginatedResponse<GetAllActivityRecordResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetAllActivityRecordResponse>>> GetActivityRecordList(SieveModel sieveModel)
        {
            var activityRecords = _dbContext.ActivityRecords.AsNoTracking();

            activityRecords = _sieveProcessor.Apply(sieveModel, activityRecords, applyPagination: false);

            var result = await activityRecords.Select(u => _mapper.Map<GetAllActivityRecordResponse>(u)).ToListAsync();

            return new Response<IEnumerable<GetAllActivityRecordResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }

        public int determineTime (DateTime activityRecord, ActivityRecordSchedule schedule)
        {
            TimeOnly date = TimeOnly.FromDateTime(activityRecord);
            TimeOnly scheduleTime1 = TimeOnly.FromDateTime(schedule.TimeNo1);
            TimeOnly scheduleTime2 = TimeOnly.FromDateTime(schedule.TimeNo2);
            TimeOnly scheduleTime3 = TimeOnly.FromDateTime(schedule.TimeNo3);

            if (date >= scheduleTime1 && date < scheduleTime2)
            {
                return 1;
            }
            else if (date >= scheduleTime2 && date < scheduleTime3)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
}
