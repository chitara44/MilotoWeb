using CBTW.Application.Interfaces.Repositories;
using CBTW.Domain.Entities;
using CBTW.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Infrastructure.Repositories;

public class ProspectRepository : IProspectRepository
{
    private readonly MilotoDbContext _db;
    public ProspectRepository(MilotoDbContext db) => _db = db;

    public Task<List<Prospect>> GetByDrawIdAsync(int drawId, CancellationToken ct = default)
    {
        var param = new Npgsql.NpgsqlParameter("p_drawid", NpgsqlTypes.NpgsqlDbType.Integer) { Value = drawId };
        return _db.Prospects
                  .FromSqlRaw("SELECT * FROM public.fn_get_prospects(@p_drawid)", param)
                  .AsNoTracking()
                  .ToListAsync(ct);
    }
}




