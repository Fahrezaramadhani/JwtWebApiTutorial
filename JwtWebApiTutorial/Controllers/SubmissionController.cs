using JwtWebApiTutorial.Exceptions;
using JwtWebApiTutorial.Requests.Submission;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Submission;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace JwtWebApiTutorial.Controllers
{
    [Route("api/submission")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _service;

        public SubmissionController(ISubmissionService service)
        {
            _service = service;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<ActionResult<Response<string>>> CreateSubmission(PostSubmissionRequest request)
        {
            try
            {
                var result = await _service.AddSubmission(request);

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
        public async Task<ActionResult<Response<string>>> EditApprovalSubmission(PutApprovalSubmissionRequest request)
        {
            try
            {
                var result = await _service.PutApprovalSubmission(request);

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
        public async Task<ActionResult<Response<string>>> EditSubmission(PutSubmissionRequest request)
        {
            try
            {
                var result = await _service.PutSubmission(request);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpGet("approval")]
        public async Task<ActionResult<Response<PaginatedResponse<GetApprovalSubmissionResponse>>>> GetApprovalSubmissionList([FromQuery] SieveModel sieveModel)
        {
            if (sieveModel.PageSize == null)
            {
                return Ok(await _service.GetApprovalSubmissionList(sieveModel));
            }
            return Ok(await _service.GetPagedApprovalSubmissionList(sieveModel));
        }

        [HttpGet]
        public async Task<ActionResult<Response<PaginatedResponse<GetHistorySubmissionResponse>>>> GetHistorySubmissionList([FromQuery] SieveModel sieveModel)
        {
            if (sieveModel.PageSize == null)
            {
                return Ok(await _service.GetSubmissionList(sieveModel));
            }
            return Ok(await _service.GetPagedSubmissionList(sieveModel));
        }
    }
}
