using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBTW.Domain.Entities;

namespace CBTW.Application.Interfaces.Repositories;

public interface IDrawRepository
{
    Task<List<Draw>> GetAllAsync(CancellationToken ct = default);
    Task<int> InsertBatchAsync(int[] drawIds, int[] n1, int[] n2, int[] n3, int[] n4, int[] n5, CancellationToken ct = default);
    Task<int?> GetLastDrawIdAsync(CancellationToken ct = default);
    Task<bool> DeleteByIdAsync(int drawId, CancellationToken ct = default);
    Task<int> UpdateAsync(int old_drawId, Draw candidate, CancellationToken ct = default);
}


