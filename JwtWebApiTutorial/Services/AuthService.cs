using JwtWebApiTutorial.Configurations;
using JwtWebApiTutorial.Constants;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Helpers;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Auth;
using JwtWebApiTutorial.Responses.Auth;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace JwtWebApiTutorial.Services
{
    public class AuthService : IAuthService
    {
        public static User user = new User();
        public static UserProfile userProfile = new UserProfile();
        private readonly DataContext _dbContext;
        private readonly JWTConfiguration _jwtConfiguration;
        public AuthService (DataContext _context, IOptions<JWTConfiguration> jwtConfiguration)
        {
            _dbContext = _context;
            _jwtConfiguration = jwtConfiguration.Value;
        }

        public async Task<PostRegisterResponse> Register(PostRegisterRequest registerRequest)
        {
            var LastUser = _dbContext.Users.OrderByDescending(user => user.Id).FirstOrDefault();

            //Check either the table user is null or not
            if (LastUser != null)
            {
                user.Id = LastUser.Id + 1;
            }

            //Add the value in request to user model
            user.Name = registerRequest.Name;
            user.Email = registerRequest.Email;
            user.Role = registerRequest.Role;
            user.Password = AuthenticationHelper.EncryptPassword(registerRequest.Password);

            //Add the user to database
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return new PostRegisterResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<PostLoginResponse> Login(PostLoginRequest loginRequest)
        {
            var _user = _dbContext.Users.FirstOrDefault(u => u.Email == loginRequest.Email);

            //Add user data to user profile
            userProfile.Name = _user.Name;
            userProfile.Religion = _user.Religion;
            userProfile.Position = _user.Position;
            userProfile.Current_salary = _user.Current_salary;
            userProfile.Status = _user.Status;
            userProfile.Join_date = _user.Join_date;
            userProfile.End_date = _user.End_date;
            userProfile.Phone_number = _user.Phone_number;
            userProfile.Email = _user.Email;
            userProfile.Address = _user.Address;
            userProfile.City = _user.City;
            userProfile.No_ktp = _user.No_ktp;
            userProfile.Npwp = _user.Npwp;
            userProfile.Date_of_birth = _user.Date_of_birth;
            userProfile.Role = _user.Role;

            //Check either the user is found or not
            if (_user == null)
            {
                return new PostLoginResponse
                {
                    message = "User not found",
                    status = 404,
                    user = userProfile,
                    access_token = null,
                    refresh_token = null,
                };
            }

            //Check either the password is correct or not
            var IsValid = AuthenticationHelper.VerifyPassword(loginRequest.Password, _user.Password);
            if (!IsValid)
            {
                return new PostLoginResponse
                {
                    message = "Unauthorized",
                    status = 401,
                    user = userProfile,
                    access_token = null,
                    refresh_token = null,
                };
            }

            //Create token for access token
            var token = AuthenticationHelper.GenerateJwtToken(
                _user.Id,
                _jwtConfiguration.SecretKey,
                DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpirationTime));

            //Create token for refresh token
            var refreshToken = AuthenticationHelper.GenerateJwtToken(
                _user.Id,
                _jwtConfiguration.RefreshSecretKey,
                DateTime.UtcNow.AddDays(_jwtConfiguration.RefreshExpirationTime));

            _user.Refresh_token = refreshToken;
            await _dbContext.SaveChangesAsync();

            return new PostLoginResponse
            {
                message = "OK",
                status = 200,
                user = userProfile,
                access_token = token,
                refresh_token = refreshToken,
            };
        }

        public async Task<PostRefreshResponse> Refresh(PostRefreshRequest refreshRequest)
        {
            var _user = _dbContext.Users.FirstOrDefault(x => x.Refresh_token == refreshRequest.refresh_token);

            //Check either the user is found or not
            if (_user == null)
            {
                return new PostRefreshResponse
                {
                    message = "User not found",
                    status = 404,
                    access_token = null,
                };
            }

            //Getting claim for getting expiry date of the token
            var _claimsPrincipal = AuthenticationHelper.GetPrincipalFromExpiredToken(refreshRequest.refresh_token, _jwtConfiguration.RefreshSecretKey);
            var utcExpiryDate = long.Parse(_claimsPrincipal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

            //Create new token for access token
            var token = AuthenticationHelper.GenerateJwtToken(
                _user.Id,
                _jwtConfiguration.SecretKey,
                DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpirationTime));

            //Check either the expiry date already expired or not
            if (expiryDate > DateTime.Now)
            {
                return new PostRefreshResponse
                {
                    message = "OK",
                    status = 200,
                    access_token = token,
                };
            }else
            {
                return new PostRefreshResponse
                {
                    message = "Unauthorized",
                    status = 401,
                    access_token = null,
                };
            }
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTimeVal;
        }
    }
}
