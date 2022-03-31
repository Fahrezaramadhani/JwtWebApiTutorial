using AutoMapper;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Helpers;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Schedule;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Schedules;
using JwtWebApiTutorial.Services.Interface;
using JwtWebApiTutorial.Sieve;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;

namespace JwtWebApiTutorial.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ApplicationSieveProcessor _sieveProcessor;

        public ScheduleService(DataContext _context, IMapper mapper, ApplicationSieveProcessor sieveProcessor)
        {
            _dbContext = _context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Response<string>> Add(PostScheduleRequest postScheduleRequest)
        {
            var schedule = _mapper.Map<AttendanceSchedule>(postScheduleRequest);

            if (!_dbContext.AttendanceSchedules.Any(x => x.Name == schedule.Name))
            {
                _dbContext.AttendanceSchedules.Add(schedule);
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

        public async Task<Response<GetScheduleResponse>> Get(int id)
        {
            var _schedule = _dbContext.AttendanceSchedules.FirstOrDefault(x => x.Id == id);
            GetScheduleResponse getScheduleResponse = new GetScheduleResponse();

            //Check either the table schedule is null or not
            if (_schedule == null)
            {
                return new Response<GetScheduleResponse>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = getScheduleResponse
                };
            }

            getScheduleResponse = _mapper.Map<GetScheduleResponse>(_schedule);

            return new Response<GetScheduleResponse>
            {
                Message = "OK",
                Status = 200,
                Data = getScheduleResponse
            };
        }

        public async Task<Response<string>> Update(PutScheduleRequest putScheduleRequest)
        {
            var _schedule = await _dbContext.AttendanceSchedules.FirstOrDefaultAsync(x => x.Id == putScheduleRequest.ScheduleId);

            //Check either the table schedule is null or not
            if (_schedule == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }

            _dbContext.AttendanceSchedules.Update(_mapper.Map<AttendanceSchedule>(putScheduleRequest));
            await _dbContext.SaveChangesAsync();
            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<string>> Delete(int id)
        {
            var _schedule = await _dbContext.AttendanceSchedules.FirstOrDefaultAsync(x => x.Id == id);

            //Check either the table schedule is null or not
            if (_schedule == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }

            _dbContext.AttendanceSchedules.Remove(_schedule);
            await _dbContext.SaveChangesAsync();

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<PaginatedResponse<GetAllScheduleResponse>>> GetPagedScheduleList(SieveModel sieveModel)
        {
            var schedules = _dbContext.AttendanceSchedules.AsNoTracking();

            schedules = _sieveProcessor.Apply(sieveModel, schedules, applyPagination: false);

            int count = await schedules.CountAsync();

            schedules = _sieveProcessor.Apply(sieveModel, schedules, applyPagination: true);

            var result = await PaginationHelper.GetPagedResultAsync<AttendanceSchedule, GetAllScheduleResponse>(schedules, sieveModel, _mapper, count);

            return new Response<PaginatedResponse<GetAllScheduleResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetAllScheduleResponse>>> GetScheduleList(SieveModel sieveModel)
        {
            var schedules = _dbContext.AttendanceSchedules.AsNoTracking();

            schedules = _sieveProcessor.Apply(sieveModel, schedules, applyPagination: false);

            var result = await schedules.Select(u => _mapper.Map<GetAllScheduleResponse>(u)).ToListAsync();

            return new Response<IEnumerable<GetAllScheduleResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }
    }
}
