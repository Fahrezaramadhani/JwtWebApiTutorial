using AutoMapper;
using JwtWebApiTutorial.Constants;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Helpers;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.User;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Users;
using JwtWebApiTutorial.Services.Interface;
using JwtWebApiTutorial.Sieve;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;

namespace JwtWebApiTutorial.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ApplicationSieveProcessor _sieveProcessor;

        public UserService(DataContext _context, IMapper mapper, ApplicationSieveProcessor sieveProcessor)
        {
            _dbContext = _context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Response<string>> Add(PostUserRequest data)
        {
            if (!string.IsNullOrWhiteSpace(data.PhotoName) && UploadImageHelper.IsBase64(data.PhotoName))
            {
                data.PhotoName = UploadImageHelper.UploadBase64File($"{DateTime.Now:yyyyMMddhhmmss}", data.PhotoName, "Images/User");
            }

            if (data.PhotoName == string.Empty || data.PhotoName == null)
            {
                data.PhotoName = ImageConstant.DEFAULTIMAGE;
                data.PhotoName = UploadImageHelper.UploadBase64File("DefaultImage", data.PhotoName, "Images/User");
            }

            var user = _mapper.Map<User>(data);
            user.Password = AuthenticationHelper.EncryptPassword(data.Password);

            if (!_dbContext.Users.Any(x => x.Email == user.Email))
            {
                _dbContext.Users.Add(user);
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

        public async Task<Response<GetUserResponse>> Get(int id)
        {
            var _user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            GetUserResponse getUserResponse = new GetUserResponse();

            //Check either the table schedule is null or not
            if (_user == null)
            {
                return new Response<GetUserResponse>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = getUserResponse
                };
            }

            getUserResponse = _mapper.Map<GetUserResponse>(_user);

            return new Response<GetUserResponse>
            {
                Message = "OK",
                Status = 200,
                Data = getUserResponse
            };
        }

        public async Task<Response<string>> Update(PutUserRequest putUserRequest)
        {
            var _user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == putUserRequest.UserId);

            //Check either the table schedule is null or not
            if (_user == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }

            _dbContext.Users.Update(_mapper.Map<User>(putUserRequest));
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
            var _user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            //Check either the table schedule is null or not
            if (_user == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }

            _user.Status = "RESIGN";
            await _dbContext.SaveChangesAsync();

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<PaginatedResponse<GetAllUserResponse>>> GetPagedUserList(SieveModel sieveModel)
        {
            var users = _dbContext.Users.AsNoTracking();

            users = _sieveProcessor.Apply(sieveModel, users, applyPagination: false);

            int count = await users.CountAsync();

            users = _sieveProcessor.Apply(sieveModel, users, applyPagination: true);

            var result = await PaginationHelper.GetPagedResultAsync<User, GetAllUserResponse>(users, sieveModel, _mapper, count);

            return new Response<PaginatedResponse<GetAllUserResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetAllUserResponse>>> GetUserList(SieveModel sieveModel)
        {
            var users = _dbContext.Users.AsNoTracking();

            users = _sieveProcessor.Apply(sieveModel, users, applyPagination: false);

            var result = await users.Select(u => _mapper.Map<GetAllUserResponse>(u)).ToListAsync();

            return new Response<IEnumerable<GetAllUserResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }
    }
}
