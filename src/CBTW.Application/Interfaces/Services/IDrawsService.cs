namespace CBTW.Application.Interfaces.Services;

using CBTW.Domain.Entities;

public interface IDrawsService
{
    Task<IReadOnlyList<Draw>> GetAllAsync(CancellationToken ct = default);
    Task<bool> ValidateAsync(Draw candidate, CancellationToken ct = default);
    Task<bool> ValidateIsAWinnerAsync(Draw candidate, CancellationToken ct = default);
    Task<int> AddSingleAsync(Draw candidate, CancellationToken ct = default);
    Task<bool> DeleteAsync(int drawId, CancellationToken ct = default);
    Task<int> ScrapAndInsertNewAsync(CancellationToken ct = default);
    Task<int> UpdateAsync(int old_drawId, Draw candidate, CancellationToken ct = default);
}
