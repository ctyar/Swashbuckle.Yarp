using Microsoft.AspNetCore.Mvc;

namespace Sample;

[Route("")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet("path1")]
    public OkResult Get()
    {
        return Ok();
    }
}
