using Microsoft.AspNetCore.Mvc;

namespace Sample;

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
