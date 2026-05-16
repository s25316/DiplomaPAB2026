using GUS.REGON.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GUS.REGON.API.Controllers;

[Route("api/server")]
[ApiController]
public class ServerController : ControllerBase
{
    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetCurrentTime_Summary"]/*' />
    /// <remarks>
    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetCurrentTime_Description"]/summary' />
    /// </remarks>
    [ProducesResponseType(typeof(ServerTimeResponse), StatusCodes.Status200OK)]
    [HttpGet("time")]
    public IActionResult GetCurrentTime() => Ok(new ServerTimeResponse());
}