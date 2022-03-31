using JwtWebApiTutorial.Exceptions;
using JwtWebApiTutorial.Requests.ApplicationSetting;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.ApplicationSettings;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebApiTutorial.Controllers
{
    [Produces("application/json")]
    [Route("api/application_setting")]
    [ApiController]
    public class ApplicationSettingController : ControllerBase
    {
        private readonly IApplicationSettingService _service;

        public ApplicationSettingController(IApplicationSettingService service)
        {
            _service = service;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<ActionResult<Response<string>>> Create(PostApplicationSettingRequest postApplicationSettingRequest)
        {
            try
            {
                var result = await _service.Add(postApplicationSettingRequest);

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
        public async Task<ActionResult<Response<GetApplicationSettingResponse>>> Get(int id)
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
