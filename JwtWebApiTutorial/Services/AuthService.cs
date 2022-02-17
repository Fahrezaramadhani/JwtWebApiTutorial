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

            //Add the value in request to user object
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

            //Check either the user is found or not
            if (_user == null)
            {
                return new PostLoginResponse
                {
                    Message = "User not found",
                    Status = 404,
                    Data = userProfile,
                };
            }

            //Check either the password is correct or not
            var IsValid = AuthenticationHelper.VerifyPassword(loginRequest.Password, _user.Password);
            if (!IsValid)
            {
                return new PostLoginResponse
                {
                    Message = "Unauthorized",
                    Status = 401,
                    Data = userProfile,
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

            //Add user data to user profile
            userProfile.Id = _user.Id;
            userProfile.ScheduleId = _user.ScheduleId;
            userProfile.Name = _user.Name;
            userProfile.Position = _user.Position;
            userProfile.Role = _user.Role;
            userProfile.PhotoName = _user.PhotoName;
            userProfile.AccessToken = token;
            userProfile.RefreshToken = refreshToken;

            _user.RefreshToken = refreshToken;
            await _dbContext.SaveChangesAsync();

            return new PostLoginResponse
            {
                Message = "OK",
                Status = 200,
                Data = userProfile
            };
        }

        public async Task<PostRefreshResponse> Refresh(PostRefreshRequest refreshRequest)
        {
            var _user = _dbContext.Users.FirstOrDefault(x => x.RefreshToken == refreshRequest.RefreshToken);

            //Check either the user is found or not
            if (_user == null)
            {
                return new PostRefreshResponse
                {
                    Message = "User not found",
                    Status = 404,
                    AccessToken = null,
                };
            }

            //Getting claim for getting expiry date of the token
            var _claimsPrincipal = AuthenticationHelper.GetPrincipalFromExpiredToken(refreshRequest.RefreshToken, _jwtConfiguration.RefreshSecretKey);
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
                    Message = "OK",
                    Status = 200,
                    AccessToken = token,
                };
            }else
            {
                return new PostRefreshResponse
                {
                    Message = "Unauthorized",
                    Status = 401,
                    AccessToken = null,
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
