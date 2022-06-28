using JwtWebApiTutorial.Exceptions;
using JwtWebApiTutorial.Requests.Position;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Position;
using JwtWebApiTutorial.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebApiTutorial.Controllers
{
    [Produces("application/json")]
    [Route("api/position")]
    [ApiController]
    //[Authorize]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _service;

        public PositionController(IPositionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Response<string>>> CreatePosition(string position_name)
        {
            try
            {
                var result = await _service.AddPosition(position_name);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response<string>>> UpdatePosition(PutPositionRequest request)
        {
            try
            {
                var result = await _service.UpdatePosition(request);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetPositionResponse>>> GetPosition(int id)
        {
            try
            {
                var result = await _service.GetPosition(id);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> DeletePosition(int id)
        {
            try
            {
                var result = await _service.DeletePosition(id);

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<GetPositionResponse>>>> GetPositionList()
        {
            try
            {
                var result = await _service.GetPositionList();

                return Ok(result);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode((ex as HttpResponseException).Status, ex);
            }
        }
    }
}
