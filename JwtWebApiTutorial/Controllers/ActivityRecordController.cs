using JwtWebApiTutorial.Exceptions;
using JwtWebApiTutorial.Requests.ActivityRecord;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.ActivityRecords;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace JwtWebApiTutorial.Controllers
{
    [Produces("application/json")]
    [Route("api/activity_record")]
    [ApiController]
    [Authorize]
    public class ActivityRecordController : ControllerBase
    {
        private readonly IActivityRecordService _service;

        public ActivityRecordController(IActivityRecordService service)
        {
            _service = service;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<ActionResult<Response<string>>> Create(PostActivityRecordRequest postActivityRecordRequest)
        {
            try
            {
                var result = await _service.Add(postActivityRecordRequest);

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
        public async Task<ActionResult<Response<GetActivityRecordResponse>>> Get(int id)
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

        [HttpGet]
        public async Task<ActionResult<Response<PaginatedResponse<GetAllActivityRecordResponse>>>> GetUserList([FromQuery] SieveModel sieveModel)
        {
            if (sieveModel.PageSize == null)
            {
                return Ok(await _service.GetActivityRecordList(sieveModel));
            }

            return Ok(await _service.GetPagedActivityRecordList(sieveModel));
        }

    }
}
