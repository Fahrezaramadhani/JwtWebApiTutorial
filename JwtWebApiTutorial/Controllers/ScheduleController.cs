using JwtWebApiTutorial.Exceptions;
using JwtWebApiTutorial.Requests.Schedule;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Schedules;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace JwtWebApiTutorial.Controllers
{
    [Produces("application/json")]
    [Route("api/schedule")]
    [ApiController]
    //[Authorize]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;

        public ScheduleController(IScheduleService service)
        {
            _service = service;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<ActionResult<Response<string>>> Create(PostScheduleRequest postScheduleRequest)
        {
            try
            {
                var result = await _service.Add(postScheduleRequest);

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
        public async Task<ActionResult<Response<GetScheduleResponse>>> Get(int id)
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
        [HttpPut]
        public async Task<ActionResult<Response<string>>> Update(PutScheduleRequest putScheduleRequest)
        {
            try
            {
                var result = await _service.Update(putScheduleRequest);

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
        public async Task<ActionResult<Response<PaginatedResponse<GetAllScheduleResponse>>>> GetScheduleList([FromQuery] SieveModel sieveModel)
        {
            if (sieveModel.PageSize == null)
            {
                return Ok(await _service.GetScheduleList(sieveModel));
            }

            return Ok(await _service.GetPagedScheduleList(sieveModel));
        }
    }
}
