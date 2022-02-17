using JwtWebApiTutorial.Exceptions;
using JwtWebApiTutorial.Requests.Attendance;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebApiTutorial.Controllers
{
    [Produces("application/json")]
    [Route("api/attendance")]
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
        public async Task<ActionResult<Response>> CheckinOnline(PostCheckinOnlineRequest postCheckinOnlineRequest)
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
        public async Task<ActionResult<Response>> CheckoutOnline(PostCheckoutOnlineRequest postCheckoutOnlineRequest)
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
    }
}
