using JwtWebApiTutorial.Exceptions;
using JwtWebApiTutorial.Requests.Leave;
using JwtWebApiTutorial.Requests.SubmissionLeave;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Leave;
using JwtWebApiTutorial.Responses.SubmissionLeave;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace JwtWebApiTutorial.Controllers
{
    [Route("api/submission_leave")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class SubmissionLeaveController : ControllerBase
    {
        private readonly ISubmissionLeaveService _service;

        public SubmissionLeaveController(ISubmissionLeaveService service)
        {
            _service = service;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<ActionResult<Response<string>>> CreateLeave(PostLeaveRequest request)
        {
            try
            {
                var result = await _service.AddSubmissionLeave(request);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPut("approval")]
        public async Task<ActionResult<Response<string>>> EditApprovalLeave(PutApprovalLeaveRequest request)
        {
            try
            {
                var result = await _service.PutApprovalLeave(request);

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
        public async Task<ActionResult<Response<string>>> EditSubmissionLeave(PutSubmissionLeaveRequest request)
        {
            try
            {
                var result = await _service.PutSubmissionLeave(request);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpGet("approval")]
        public async Task<ActionResult<Response<PaginatedResponse<GetApprovalLeaveResponse>>>> GetApprovalLeaveList([FromQuery] SieveModel sieveModel)
        {
            if (sieveModel.PageSize == null)
            {
                return Ok(await _service.GetApprovalLeaveList(sieveModel));
            }
            return Ok(await _service.GetPagedApprovalLeaveList(sieveModel));
        }

        [HttpGet]
        public async Task<ActionResult<Response<PaginatedResponse<GetHistoryLeaveResponse>>>> GetHistoryLeaveList([FromQuery] SieveModel sieveModel)
        {
            if (sieveModel.PageSize == null)
            {
                return Ok(await _service.GetLeaveList(sieveModel));
            }
            return Ok(await _service.GetPagedLeaveList(sieveModel));
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpGet("total_hour")]
        public async Task<ActionResult<Response<GetTotalDaysLeaveResponse>>> GetTotalHoursSubmission([FromQuery] int month_number)
        {
            try
            {
                var result = await _service.GetTotalDaysLeave(month_number);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }
    }
}
