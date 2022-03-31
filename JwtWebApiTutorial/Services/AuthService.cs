using AutoMapper;
using JwtWebApiTutorial.Configurations;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Helpers;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Auth;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Auths;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace JwtWebApiTutorial.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dbContext;
        private readonly JWTConfiguration _jwtConfiguration;
        private readonly IMapper _mapper;
        public AuthService(DataContext _context, IOptions<JWTConfiguration> jwtConfiguration, IMapper mapper)
        {
            _dbContext = _context;
            _jwtConfiguration = jwtConfiguration.Value;
            _mapper = mapper;
        }

        public async Task<Response<PostLoginResponse>> Login(PostLoginRequest loginRequest)
        {
            var _user = _dbContext.Users.FirstOrDefault(u => u.Email == loginRequest.Email);

            PostLoginResponse postLoginResponse = new PostLoginResponse();

            //Check either the user is found or not
            if (_user == null || _user.Status == "RESIGN")
            {
                return new Response<PostLoginResponse>
                {
                    Message = "User not found",
                    Status = 404,
                    Data = postLoginResponse,
                };
            }

            //Check either the password is correct or not
            var IsValid = AuthenticationHelper.VerifyPassword(loginRequest.Password, _user.Password);
            if (!IsValid)
            {
                return new Response<PostLoginResponse>
                {
                    Message = "Unauthorized",
                    Status = 401,
                    Data = postLoginResponse,
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
            postLoginResponse = _mapper.Map<PostLoginResponse>(_user);
            postLoginResponse.AccessToken = token;
            postLoginResponse.RefreshToken = refreshToken;

            _user.RefreshToken = refreshToken;
            await _dbContext.SaveChangesAsync();

            return new Response<PostLoginResponse>
            {
                Message = "OK",
                Status = 200,
                Data = postLoginResponse
            };
        }

        public async Task<Response<PostRefreshResponse>> Refresh(string refreshRequest)
        {
            var _user = _dbContext.Users.FirstOrDefault(x => x.RefreshToken == refreshRequest);

            PostRefreshResponse postRefreshResponse = new PostRefreshResponse();

            //Check either the user is found or not
            if (_user == null)
            {
                return new Response<PostRefreshResponse>
                {
                    Message = "User not found",
                    Status = 404,
                    Data = postRefreshResponse,
                };
            }

            //Getting claim for getting expiry date of the token
            var _claimsPrincipal = AuthenticationHelper.GetPrincipalFromExpiredToken(refreshRequest, _jwtConfiguration.RefreshSecretKey);
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
                postRefreshResponse.AccessToken = token;
                return new Response<PostRefreshResponse>
                {
                    Message = "OK",
                    Status = 200,
                    Data = postRefreshResponse,
                };
            }else
            {
                return new Response<PostRefreshResponse>
                {
                    Message = "Unauthorized",
                    Status = 401,
                    Data = postRefreshResponse,
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
