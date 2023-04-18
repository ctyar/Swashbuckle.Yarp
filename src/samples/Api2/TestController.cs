using Microsoft.AspNetCore.Mvc;

namespace Api2;

[Route("")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet("path2")]
    public OkResult Get()
    {
        return Ok();
    }
}
