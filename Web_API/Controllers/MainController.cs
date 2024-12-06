using Microsoft.AspNetCore.Mvc;

namespace Web_API.API.Controllers;

[ApiController]
[Route("api")]
public class MainController : ControllerBase
{
    [HttpGet("ping")]
    public ActionResult Ping()
    {
        return Ok("Pong");
    }
}