using CBTW.Application.Interfaces.Repositories;
using CBTW.Application.Interfaces.Services;
using CBTW.Domain.Entities;


namespace CBTW.Application.Services.Implementations;

public class ProspectService : IProspectService
{
    private readonly IProspectRepository _repo;

    public ProspectService(IProspectRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<Prospect>> GetByDrawIdAsync(int drawId, CancellationToken ct = default)
    {
        return await _repo.GetByDrawIdAsync(drawId, ct);
    }
}

