using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Requests.Auth;
using JwtWebApiTutorial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using JwtWebApiTutorial.Services.Interface;
using JwtWebApiTutorial.Exceptions;
using JwtWebApiTutorial.Responses.Auths;
using JwtWebApiTutorial.Responses;

namespace JwtWebApiTutorial.Controllers
{
    [Produces("application/json")]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost("login_mobile")]
        public async Task<ActionResult<Response<PostLoginResponse>>> LoginMobile(PostLoginRequest loginRequest)
        {
            try
            {
                var result = await _service.LoginMobile(loginRequest);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost("login_web")]
        public async Task<ActionResult<Response<PostLoginResponse>>> LoginWeb(PostLoginRequest loginRequest)
        {
            try
            {
                var result = await _service.LoginWeb(loginRequest);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost("refresh/{refreshToken}")]
        public async Task<ActionResult<Response<PostRefreshResponse>>> Refresh(string refreshToken)
        {
            try
            {
                var result = await _service.Refresh(refreshToken);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }
    }
}
