using Base.Models.ValueObjects.Regony;
using Microsoft.AspNetCore.Mvc;

namespace GUS.REGON.API.Controllers;

[ApiController]
[Route("")]
public class RegonController(RegonService service) : ControllerBase
{
    [HttpGet("serverTime")]
    public IActionResult GetSeverTime() => Ok(DateTimeOffset.Now);


    [HttpGet]
    public async Task<IActionResult> GetAsync(string regonString, CancellationToken cancellationToken)
    {
        var regon = (Regon)regonString;
        var items = await service.GetAsync(regon, cancellationToken);
        return Ok(items);
    }


    [HttpGet("komunikatUslugi")]
    public async Task<IActionResult> GetKomunikatUslugiAsync(CancellationToken cancellationToken)
    {
        var result = await service.GetKomunikatUslugiAsync(cancellationToken);
        return Ok(result);
    }


    [HttpGet("statusUslugi")]
    public async Task<IActionResult> GetStatusUslugiAsync(CancellationToken cancellationToken)
    {
        var result = await service.GetStatusUslugiAsync(cancellationToken);
        return Ok(result);
    }


    [HttpGet("statusSesji")]
    public async Task<IActionResult> GetStatusSesjiAsync(CancellationToken cancellationToken)
    {
        var result = await service.GetStatusSesjiAsync(cancellationToken);
        return Ok(result);
    }


    [HttpGet("stanDanych")]
    public async Task<IActionResult> GetStanDanychAsync(CancellationToken cancellationToken)
    {
        var result = await service.GetStanDanychAsync(cancellationToken);
        return Ok(result);
    }
}