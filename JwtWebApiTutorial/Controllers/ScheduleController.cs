using JwtWebApiTutorial.Exceptions;
using JwtWebApiTutorial.Requests.Schedule;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebApiTutorial.Controllers
{
    [Produces("application/json")]
    [Route("api/schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;

        public ScheduleController(IScheduleService service)
        {
            _service = service;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost("create")]
        public async Task<ActionResult<Response>> Create(PostScheduleRequest postScheduleRequest)
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
    }
}
