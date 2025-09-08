using CBTW.Application.Interfaces.Services;
using CBTW.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CBTW.Api.Controllers;

[ApiController]
[Route("api/v1/prospects")]
public class ProspectsController : ControllerBase
{
    private readonly IProspectService _service;
    public ProspectsController(IProspectService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Prospect>>> Get([FromQuery] int drawId, CancellationToken ct)
    {
        var items = await _service.GetByDrawIdAsync(drawId, ct);
        return Ok(items);
    }
}

