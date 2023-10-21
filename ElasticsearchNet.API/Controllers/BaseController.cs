using ElasticsearchNet.API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ElasticsearchNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult AddActionResult<T>(ResponseDto<T> response )
        {
            if (response.StatusCode == HttpStatusCode.NoContent) return new ObjectResult(null) { StatusCode = response.StatusCode.GetHashCode() };
            return new ObjectResult(response) { StatusCode=response.StatusCode.GetHashCode() };
        }
    }
}
