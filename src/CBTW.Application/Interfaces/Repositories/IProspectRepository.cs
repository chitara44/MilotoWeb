using CBTW.Domain.Entities;

namespace CBTW.Application.Interfaces.Repositories;
public interface IProspectRepository
{
    Task<List<Prospect>> GetByDrawIdAsync(int drawId, CancellationToken ct = default);
}

