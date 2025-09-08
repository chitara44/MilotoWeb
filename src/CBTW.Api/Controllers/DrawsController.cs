using CBTW.Application.Services.Implementations;
using CBTW.Application.Interfaces.Services;
using CBTW.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CBTW.Api.Controllers;


[ApiController]
[Route("api/v1/draws")]
public class DrawsController : ControllerBase
{
    private readonly IDrawsService _service;
    public DrawsController(IDrawsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Draw>>> Get(CancellationToken ct)
    {
        var items = await _service.GetAllAsync(ct);
        return Ok(items);
    }

    [HttpPost("validate")]
    public async Task<ActionResult<bool>> Validate([FromBody] Draw candidate, CancellationToken ct)
    {
        var ok = await _service.ValidateAsync(candidate, ct);
        return Ok(ok);
    }

    [HttpPost("winner")]
    public async Task<ActionResult<bool>> ValidateIsAWinner([FromBody] Draw candidate, CancellationToken ct)
    {
        var ok = await _service.ValidateIsAWinnerAsync(candidate, ct);
        return Ok(ok);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Draw candidate, CancellationToken ct)
    {
        var inserted = await _service.AddSingleAsync(candidate, ct);
        return CreatedAtAction(nameof(Get), new { id = inserted }, inserted);
    }

    [HttpPut("update")]
    public async Task<ActionResult> Update([FromQuery] int old_drawId, [FromBody] Draw candidate, CancellationToken ct)
    {
        int updated = await _service.UpdateAsync(old_drawId, candidate, ct);

        if (updated <= 0)
        {
            return NotFound(new { message = $"Draw with ID {old_drawId} not found" });
        }

        return Ok(new { message = $"Draw updated successfully {updated} records" });
    }


    [HttpDelete("{drawId:int}")]
    public async Task<ActionResult> Delete(int drawId, CancellationToken ct)
    {
        var ok = await _service.DeleteAsync(drawId, ct);
        return ok ? NoContent() : NotFound();
    }

    [HttpPost("scrap-new")]
    public async Task<ActionResult<int>> ScrapNew(CancellationToken ct)
    {
        var count = await _service.ScrapAndInsertNewAsync(ct);
        return Ok(count);
    }
}
