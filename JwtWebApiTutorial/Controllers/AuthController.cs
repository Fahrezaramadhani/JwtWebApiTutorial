using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Requests.Auth;
using JwtWebApiTutorial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using JwtWebApiTutorial.Responses.Auth;
using JwtWebApiTutorial.Services.Interface;
using JwtWebApiTutorial.Exceptions;

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
        [HttpPost("register")]
        public async Task<ActionResult<PostRegisterResponse>> Register(PostRegisterRequest registerRequest)
        {
            try
            {
                var result = await _service.Register(registerRequest);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost("login")]
        public async Task<ActionResult<PostLoginResponse>> Login(PostLoginRequest loginRequest)
        {
            try
            {
                var result = await _service.Login(loginRequest);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost("Refresh")]
        public async Task<ActionResult<PostLoginResponse>> Refresh(PostRefreshRequest refreshRequest)
        {
            try
            {
                var result = await _service.Refresh(refreshRequest);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }
    }
}
