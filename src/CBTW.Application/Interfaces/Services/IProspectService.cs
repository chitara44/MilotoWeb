using CBTW.Domain.Entities;

namespace CBTW.Application.Interfaces.Services;

public interface IProspectService
{
    Task<IReadOnlyList<Prospect>> GetByDrawIdAsync(int drawId, CancellationToken ct = default);
}

