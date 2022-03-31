using JwtWebApiTutorial.Exceptions;
using JwtWebApiTutorial.Requests.Attendance;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Attendances;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace JwtWebApiTutorial.Controllers
{
    [Produces("application/json")]
    [Route("api/attendance")]
    [Authorize]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost("checkin_online")]
        [Authorize]
        public async Task<ActionResult<Response<string>>> CheckinOnline(PostCheckinOnlineRequest postCheckinOnlineRequest)
        {
            try
            {
                var result = await _service.CheckinOnline(postCheckinOnlineRequest);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost("checkout_online")]
        public async Task<ActionResult<Response<string>>> CheckoutOnline(PostCheckoutOnlineRequest postCheckoutOnlineRequest)
        {
            try
            {
                var result = await _service.CheckoutOnline(postCheckoutOnlineRequest);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpPost("checkin_offline")]
        public async Task<ActionResult<Response<string>>> CheckinOffline(PostCheckinOfflineRequest postCheckinOfflineRequest)
        {
            try
            {
                var result = await _service.CheckinOffline(postCheckinOfflineRequest);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpPost("checkout_offline")]
        public async Task<ActionResult<Response<string>>> CheckoutOffline(PostCheckoutOfflineRequest postCheckoutOfflineRequest)
        {
            try
            {
                var result = await _service.CheckoutOffline(postCheckoutOfflineRequest);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Response<PaginatedResponse<GetAllAttendanceResponse>>>> GetUserList([FromQuery] SieveModel sieveModel)
        {
            if (sieveModel.PageSize == null)
            {
                return Ok(await _service.GetAttendanceList(sieveModel));
            }

            return Ok(await _service.GetPagedAttendanceList(sieveModel));
        }
    }
}
