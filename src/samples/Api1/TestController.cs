using Microsoft.AspNetCore.Mvc;

namespace Api1;

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
