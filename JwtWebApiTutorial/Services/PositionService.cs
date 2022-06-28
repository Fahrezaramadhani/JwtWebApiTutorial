using AutoMapper;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Position;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Position;
using JwtWebApiTutorial.Services.Interface;
using JwtWebApiTutorial.Sieve;
using Microsoft.EntityFrameworkCore;

namespace JwtWebApiTutorial.Services
{
    public class PositionService : IPositionService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ApplicationSieveProcessor _sieveProcessor;

        public PositionService(DataContext _context, IMapper mapper, ApplicationSieveProcessor sieveProcessor)
        {
            _dbContext = _context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Response<string>> AddPosition(string request)
        {
            var _positions = await _dbContext.Positions.FirstOrDefaultAsync(x => x.PositionName == request);
            if (_positions != null)
            {
                return new Response<string>
                {
                    Message = "Conflict",
                    Status = 409,
                    Data = ""
                };
            }
            Position position = new Position();
            position.PositionName = request;

            _dbContext.Positions.Add(position);
            await _dbContext.SaveChangesAsync();

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<GetPositionResponse>> GetPosition(int id)
        {
            var _position = await _dbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);
            GetPositionResponse response = new GetPositionResponse();

            if (_position == null)
            {
                return new Response<GetPositionResponse>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = response
                };
            }

            response = _mapper.Map<GetPositionResponse>(_position);

            return new Response<GetPositionResponse>
            {
                Message = "OK",
                Status = 200,
                Data = response
            };
        }

        public async Task<Response<List<GetPositionResponse>>> GetPositionList()
        {
            var _positions = await _dbContext.Positions.ToListAsync();
            List<GetPositionResponse> response = new List<GetPositionResponse>();

            if (_positions == null)
            {
                return new Response<List<GetPositionResponse>>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = response
                };
            }

            response = _mapper.Map<List<Position>, List<GetPositionResponse>>(_positions);

            return new Response<List<GetPositionResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = response
            };
        }

        public async Task<Response<string>> UpdatePosition(PutPositionRequest request)
        {
            var _position = await _dbContext.Positions.FirstOrDefaultAsync(x => x.Id == request.PositionId);

            if (_position == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }

            _position.PositionName = request.PositionName;
            _dbContext.Positions.Update(_position);
            await _dbContext.SaveChangesAsync();

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<string>> DeletePosition(int id)
        {
            var _position = await _dbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);

            if (_position == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }

            var _users = await _dbContext.Users.FirstOrDefaultAsync(x => x.PositionId == id);

            if (_users != null)
            {
                return new Response<string>
                {
                    Message = "Conflict",
                    Status = 409,
                    Data = ""
                };
            }

            _dbContext.Positions.Remove(_position);
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
