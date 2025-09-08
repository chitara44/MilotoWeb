using CBTW.Application.Interfaces.Repositories;
using CBTW.Application.Interfaces.Services;
using CBTW.Domain.Entities;

namespace CBTW.Application.Services.Implementations;

public class DrawsService : IDrawsService
{
    private readonly IDrawRepository _repository;

    public DrawsService(IDrawRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IReadOnlyList<Draw>> GetAllAsync(CancellationToken ct = default)
    {
        return await _repository.GetAllAsync(ct);
    }

    public async Task<bool> ValidateAsync(Draw candidate, CancellationToken ct = default)
    {
        var existingDraws = await _repository.GetAllAsync(ct);

        // Validation: return false if the same numbers already exist
        var isDuplicate = existingDraws.Any(d =>
            d.N1 == candidate.N1 &&
            d.N2 == candidate.N2 &&
            d.N3 == candidate.N3 &&
            d.N4 == candidate.N4 &&
            d.N5 == candidate.N5);

        return !isDuplicate;
    }

    public async Task<bool> ValidateIsAWinnerAsync(Draw candidate, CancellationToken ct = default)
    {
        var existingDraws = await _repository.GetAllAsync(ct);

        // Validation: return true if the same numbers already exist
        var isAWinner = existingDraws.Any(d =>
            d.DrawId == candidate.DrawId &&
            d.N1 == candidate.N1 &&
            d.N2 == candidate.N2 &&
            d.N3 == candidate.N3 &&
            d.N4 == candidate.N4 &&
            d.N5 == candidate.N5);

        return isAWinner;
    }

    public async Task<int> AddSingleAsync(Draw candidate, CancellationToken ct = default)
    {
        // Validate first
        if (!await ValidateAsync(candidate, ct))
            throw new InvalidOperationException("Duplicate draw cannot be added.");

        // Save using batch insert with one record
        return await _repository.InsertBatchAsync(
            new[] { candidate.DrawId },
            new[] { candidate.N1 },
            new[] { candidate.N2 },
            new[] { candidate.N3 },
            new[] { candidate.N4 },
            new[] { candidate.N5 },
            ct
        );
    }

    public Task<bool> DeleteAsync(int drawId, CancellationToken ct = default)
    {
        return _repository.DeleteByIdAsync(drawId, ct);
    }

    public async Task<int> ScrapAndInsertNewAsync(CancellationToken ct = default)
    {
        // TODO: implement actual scraping logic
        // for now, return 0 (stub)
        return await Task.FromResult(0);
    }

    public async Task<int> UpdateAsync(int oldDrawId, Draw candidate, CancellationToken ct)
    {
        return await _repository.UpdateAsync(oldDrawId, candidate, ct);
    }

}




