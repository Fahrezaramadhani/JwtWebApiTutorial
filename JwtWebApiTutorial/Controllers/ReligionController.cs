using JwtWebApiTutorial.Exceptions;
using JwtWebApiTutorial.Requests.Religion;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Religion;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebApiTutorial.Controllers
{
    [Route("api/religion")]
    [ApiController]
    public class ReligionController : ControllerBase
    {
        private readonly IReligionService _service;

        public ReligionController(IReligionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Response<string>>> CreateReligion(string religion_name)
        {
            try
            {
                var result = await _service.AddReligion(religion_name);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response<string>>> UpdateReligion(PutReligionRequest request)
        {
            try
            {
                var result = await _service.UpdateReligion(request);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetReligionResponse>>> GetReligion(int id)
        {
            try
            {
                var result = await _service.GetReligion(id);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> DeleteReligion(int id)
        {
            try
            {
                var result = await _service.DeleteReligion(id);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<GetReligionResponse>>>> GetReligionList()
        {
            try
            {
                var result = await _service.GetReligionList();

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }
    }
}
