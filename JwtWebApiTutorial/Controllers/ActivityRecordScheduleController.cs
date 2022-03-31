using JwtWebApiTutorial.Exceptions;
using JwtWebApiTutorial.Requests.ActivityRecordSchedule;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.ActivityRecordSchedules;
using JwtWebApiTutorial.Responses.Schedules;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebApiTutorial.Controllers
{
    [Produces("application/json")]
    [Route("api/activity_record_schedule")]
    [ApiController]
    public class ActivityRecordScheduleController : ControllerBase
    {
        private readonly IActivityRecordScheduleService _service;

        public ActivityRecordScheduleController(IActivityRecordScheduleService service)
        {
            _service = service;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<ActionResult<Response<string>>> Create(PostActivityRecordScheduleRequest postActivityRecordScheduleRequest)
        {
            try
            {
                var result = await _service.Add(postActivityRecordScheduleRequest);

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
        public async Task<ActionResult<Response<GetActivityRecordScheduleResponse>>> Get(int id)
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
    }
}
