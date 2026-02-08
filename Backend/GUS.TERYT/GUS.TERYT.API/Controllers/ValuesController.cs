using GUS.TERYT.Files;
using GUS.TERYT.Models.Requests.Parameters;
using Microsoft.AspNetCore.Mvc;
using AdaptedTerc = GUS.TERYT.Files.Models.Adapted.Teryt.Terc;
using AdaptedUlicInfo = GUS.TERYT.Files.Models.Adapted.Teryt.UlicInfo;
using SourceSimc = GUS.TERYT.Files.Models.Source.Teryt.Simc;
using SourceTerc = GUS.TERYT.Files.Models.Source.Teryt.Terc;
using SourceUlic = GUS.TERYT.Files.Models.Source.Teryt.Ulic;

namespace GUS.TERYT.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet("test")]
    public async Task<IActionResult> GetTercAsync(
        [FromQuery] PowiatParameters powiatParameters,
        CancellationToken cancellationToken)
    {
        return Ok(powiatParameters);
    }

    [HttpGet("terc/a")]
    public async Task<IActionResult> GetTercAsync(CancellationToken cancellationToken)
    {
        var path = @"C:\Users\User\Downloads\teryt\TERC_Adresowy_2026-01-29.csv";
        var reader = new TerytAdaptedReader<AdaptedTerc>(path);
        var items = await reader.ReadAllAsync();
        return Ok(value: items);
    }

    [HttpGet("terc/s")]
    public async Task<IActionResult> GetTerc2Async(CancellationToken cancellationToken)
    {
        //var path = @"C:\Users\User\Downloads\teryt\TERC_Adresowy_2026-01-29 — kopia.csv";
        var path = @"C:\Users\User\Downloads\teryt\TERC_Adresowy_2026-01-29.csv";
        var reader = new TerytSourceReader<SourceTerc>(path);
        var items = await reader.ReadAllAsync();
        return Ok(value: items);
    }

    /*
        [HttpGet("simc/a")]
        public async Task<IActionResult> GetSimcJsonFileAsync()
        {
            var path = @"C:\Users\User\Downloads\teryt\SIMC_Adresowy_2026-01-29.csv";
            var reader = new TerytAdaptedReader<AdaptedSimc>(path);
            var items = await reader.ReadAllAsync();

            // Serializacja do stringa (uwaga na pamięć RAM przy ogromnych plikach!)
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
            var bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);
            var stream = new System.IO.MemoryStream(bytes);

            return File(stream, "application/json", "simc_data.json");
        }
    */
    /*[HttpGet("simc/a")]
    public async Task<IActionResult> GetSimcAsync(CancellationToken cancellationToken)
    {
        var path = @"C:\Users\User\Downloads\teryt\SIMC_Adresowy_2026-01-29.csv";
        var reader = new TerytAdaptedReader<AdaptedSimc>(path);
        var items = await reader.ReadAllAsync();
        return Ok(items);
    }*/

    [HttpGet("simc/s")]
    public async Task<IActionResult> GetSimc2Async(CancellationToken cancellationToken)
    {
        var path = @"C:\Users\User\Downloads\teryt\SIMC_Adresowy_2026-01-29.csv";
        var reader = new TerytSourceReader<SourceSimc>(path);
        var items = await reader.ReadAllAsync();
        return Ok(items);
    }

    [HttpGet("ulic/a")]
    public async Task<IActionResult> GetUlicAsync(CancellationToken cancellationToken)
    {
        var path = @"C:\Users\User\Downloads\teryt\ULIC_Adresowy_2026-01-29.csv";
        var reader = new TerytAdaptedReader<AdaptedUlicInfo>(path);
        var items = await reader.ReadAllAsync();
        return Ok(items);
    }

    [HttpGet("ulic/s")]
    public async Task<IActionResult> GetUlic2Async(CancellationToken cancellationToken)
    {
        var path = @"C:\Users\User\Downloads\teryt\ULIC_Adresowy_2026-01-29.csv";
        var reader = new TerytSourceReader<SourceUlic>(path);
        var items = await reader.ReadAllAsync();
        return Ok(items);
    }
}
