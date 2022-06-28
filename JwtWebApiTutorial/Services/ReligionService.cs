using AutoMapper;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Religion;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Religion;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace JwtWebApiTutorial.Services
{
    public class ReligionService : IReligionService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public ReligionService(DataContext _context, IMapper mapper)
        {
            _dbContext = _context;
            _mapper = mapper;
        }

        public async Task<Response<string>> AddReligion(string request)
        {
            var _religions = await _dbContext.Religions.FirstOrDefaultAsync(x => x.ReligionName == request);
            if (_religions != null)
            {
                return new Response<string>
                {
                    Message = "Conflict",
                    Status = 409,
                    Data = ""
                };
            }
            Religion religion = new Religion();
            religion.ReligionName = request;

            _dbContext.Religions.Add(religion);
            await _dbContext.SaveChangesAsync();

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<GetReligionResponse>> GetReligion(int id)
        {
            var _religion = await _dbContext.Religions.FirstOrDefaultAsync(x => x.Id == id);
            GetReligionResponse response = new GetReligionResponse();

            if (_religion == null)
            {
                return new Response<GetReligionResponse>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = response
                };
            }

            response = _mapper.Map<GetReligionResponse>(_religion);

            return new Response<GetReligionResponse>
            {
                Message = "OK",
                Status = 200,
                Data = response
            };
        }

        public async Task<Response<List<GetReligionResponse>>> GetReligionList()
        {
            var _religions = await _dbContext.Religions.ToListAsync();
            List<GetReligionResponse> response = new List<GetReligionResponse>();

            if (_religions == null)
            {
                return new Response<List<GetReligionResponse>>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = response
                };
            }

            response = _mapper.Map<List<Religion>, List<GetReligionResponse>>(_religions);

            return new Response<List<GetReligionResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = response
            };
        }

        public async Task<Response<string>> UpdateReligion(PutReligionRequest request)
        {
            var _religion = await _dbContext.Religions.FirstOrDefaultAsync(x => x.Id == request.ReligionId);

            if (_religion == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }

            _religion.ReligionName = request.ReligionName;
            _dbContext.Religions.Update(_religion);
            await _dbContext.SaveChangesAsync();

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<string>> DeleteReligion(int id)
        {
            var _religion = await _dbContext.Religions.FirstOrDefaultAsync(x => x.Id == id);

            if (_religion == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }

            var _users = await _dbContext.Users.FirstOrDefaultAsync(x => x.ReligionId == id);

            if (_users != null)
            {
                return new Response<string>
                {
                    Message = "Conflict",
                    Status = 409,
                    Data = ""
                };
            }

            _dbContext.Religions.Remove(_religion);
            await _dbContext.SaveChangesAsync();

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }
    }
}
