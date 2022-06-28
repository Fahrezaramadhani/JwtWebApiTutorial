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
                data.PhotoName = UploadImageHelper.UploadBase64File($"{DateTime.Now:yyyyMMddhhmmss}", data.PhotoName, "Images/User");
            }

            if (data.Status == "Permanent")
            {
                data.EndDate = new DateTime(0001, 01, 01);
            }

            var user = _mapper.Map<User>(data);

            user.Password = AuthenticationHelper.EncryptPassword(data.Password);

            if (!_dbContext.Users.Any(x => x.Email == user.Email))
            {
                var userSuperior = _dbContext.Users.FirstOrDefault(x => x.Id == user.SuperiorId);
                if (userSuperior != null)
                {
                    userSuperior.SuperiorId = 0;
                    _dbContext.Users.Update(userSuperior);
                }
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
            var _user = await _dbContext.Users.Include(p => p.Position).Include(r => r.Religion).FirstOrDefaultAsync(x => x.Id == id);

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

        public async Task<Response<List<GetSuperiorResponse>>> GetSuperiorList()
        {
            var _users = await _dbContext.Users.Where(u => u.Role == "Employee" && u.Status != "Resign").ToListAsync();
            List<GetSuperiorResponse> users = new List<GetSuperiorResponse>();
            if (_users.Any())
            {
                foreach (var user in _users)
                {
                    GetSuperiorResponse superior = new GetSuperiorResponse();
                    superior.UserId = user.Id;
                    superior.UserName = user.Name;
                    users.Add(superior);
                }

                return new Response<List<GetSuperiorResponse>>
                {
                    Message = "OK",
                    Status = 200,
                    Data = users
                };
            }
            else
            {
                return new Response<List<GetSuperiorResponse>>
                {
                    Message = "OK",
                    Status = 200,
                    Data = users
                };
            }
        }

        public async Task<Response<string>> Update(PutUserRequest putUserRequest)
        {
            var _user = _dbContext.Users.FirstOrDefault(x => x.Id == putUserRequest.UserId);

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

            if (_user.PhotoName != putUserRequest.PhotoName)
            {
                UploadImageHelper.DeleteImage(_user.PhotoName);
                if (!string.IsNullOrWhiteSpace(putUserRequest.PhotoName) && UploadImageHelper.IsBase64(putUserRequest.PhotoName))
                {
                    putUserRequest.PhotoName = UploadImageHelper.UploadBase64File($"{DateTime.Now:yyyyMMddhhmmss}", putUserRequest.PhotoName, "Images/User");
                }

                if (putUserRequest.PhotoName == string.Empty || putUserRequest.PhotoName == null)
                {
                    putUserRequest.PhotoName = ImageConstant.DEFAULTIMAGE;
                    putUserRequest.PhotoName = UploadImageHelper.UploadBase64File($"{DateTime.Now:yyyyMMddhhmmss}", putUserRequest.PhotoName, "Images/User");
                }
            }

            if (putUserRequest.Password == "")
            {
                putUserRequest.Password = _user.Password;
            }
            else
            {
                putUserRequest.Password = AuthenticationHelper.EncryptPassword(putUserRequest.Password);
            }

            var position = _dbContext.Positions.Where(u => u.PositionName == putUserRequest.Position).FirstOrDefault();
            var religion = _dbContext.Religions.Where(u => u.ReligionName == putUserRequest.Religion).FirstOrDefault();
            if (putUserRequest.Status == "Permanent")
            {
                putUserRequest.EndDate = new DateTime(0001, 01, 01);
            }
            _user.Name = putUserRequest.Name;
            _user.Email = putUserRequest.Email;
            _user.Password = putUserRequest.Password;
            _user.NoKTP = putUserRequest.NoKtp;
            _user.Address = putUserRequest.Address;
            _user.City = putUserRequest.City;
            _user.Role = putUserRequest.Role;
            _user.Gender = putUserRequest.Gender;
            _user.DateOfBirth = putUserRequest.DateOfBirth;
            _user.JoinDate = putUserRequest.JoinDate;
            _user.EndDate = putUserRequest.EndDate;
            _user.PhoneNumber = putUserRequest.PhoneNumber;
            _user.PhotoName = putUserRequest.PhotoName;
            _user.SuperiorId = putUserRequest.SuperiorId;
            _user.Status = putUserRequest.Status;
            _user.ReligionId = religion.Id;
            _user.PositionId = position.Id;
            _user.NPWP = putUserRequest.Npwp;
            var userSuperior = _dbContext.Users.FirstOrDefault(x => x.Id == _user.SuperiorId);
            if (userSuperior != null)
            {
                userSuperior.SuperiorId = 0;
                _dbContext.Users.Update(userSuperior);
            }
            /*
            user.PositionId = position.Id;
            user.ReligionId = religion.Id;
            */

            _dbContext.Users.Update(_user);
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

            _user.Status = "Resign";
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
            var users = _dbContext.Users.Include(p => p.Position).Include(r => r.Religion).AsNoTracking();

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
            var users = _dbContext.Users.Include(p => p.Position).Include(r => r.Religion).AsNoTracking();

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
