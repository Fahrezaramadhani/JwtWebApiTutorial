using JwtWebApiTutorial.Exceptions;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.User;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Users;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace JwtWebApiTutorial.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<ActionResult<Response<string>>> Create(PostUserRequest postUserRequest)
        {
            try
            {
                var result = await _service.Add(postUserRequest);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPut]
        public async Task<ActionResult<Response<string>>> Update(PutUserRequest putUserRequest)
        {
            try
            {
                var result = await _service.Update(putUserRequest);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetUserResponse>>> Get(int id)
        {
            try
            {
                var result = await _service.Get(id);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> Delete(int id)
        {
            try
            {
                var result = await _service.Delete(id);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Response<PaginatedResponse<GetAllUserResponse>>>> GetUserList([FromQuery] SieveModel sieveModel)
        {
            if (sieveModel.PageSize == null)
            {
                return Ok(await _service.GetUserList(sieveModel));
            }

            return Ok(await _service.GetPagedUserList(sieveModel));
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpGet("superior")]
        public async Task<ActionResult<Response<List<GetSuperiorResponse>>>> GetSuperiorList()
        {
            try
            {
                var result = await _service.GetSuperiorList();

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }
    }
}
