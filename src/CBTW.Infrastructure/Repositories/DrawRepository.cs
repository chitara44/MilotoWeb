using CBTW.Application.Interfaces.Repositories;
using CBTW.Domain.Entities;
using CBTW.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace CBTW.Infrastructure.Repositories;

public class DrawRepository(MilotoDbContext db) : IDrawRepository
{
    public Task<List<Draw>> GetAllAsync(CancellationToken ct = default)
    {
        return db.Draws
                    .FromSqlRaw("SELECT * FROM public.fn_get_draws()")
                    .AsNoTracking()
                    .ToListAsync(ct);
    }

    public async Task<int> InsertBatchAsync(int[] drawIds, int[] n1, int[] n2, int[] n3, int[] n4, int[] n5, CancellationToken ct = default)
    {
        var conn = db.Database.GetDbConnection();
        await using (conn)
        {
            if (conn.State != System.Data.ConnectionState.Open)
                await conn.OpenAsync(ct);

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "CALL public.sp_insert_draw_batch(@p_drawid, @p_n1, @p_n2, @p_n3, @p_n4, @p_n5)";
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.Parameters.Add(new NpgsqlParameter("p_drawid", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer) { Value = drawIds });
            cmd.Parameters.Add(new NpgsqlParameter("p_n1", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer) { Value = n1 });
            cmd.Parameters.Add(new NpgsqlParameter("p_n2", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer) { Value = n2 });
            cmd.Parameters.Add(new NpgsqlParameter("p_n3", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer) { Value = n3 });
            cmd.Parameters.Add(new NpgsqlParameter("p_n4", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer) { Value = n4 });
            cmd.Parameters.Add(new NpgsqlParameter("p_n5", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer) { Value = n5 });
            var result = await cmd.ExecuteNonQueryAsync(ct);

            return result;
        }
    }

    public async Task<int?> GetLastDrawIdAsync(CancellationToken ct = default)
    {
        var conn = db.Database.GetDbConnection();
        await using (conn)
        {
            if (conn.State != System.Data.ConnectionState.Open)
                await conn.OpenAsync(ct);

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT public.fn_get_last_drawid()";
            var scalar = await cmd.ExecuteScalarAsync(ct);
            return scalar == DBNull.Value || scalar == null ? null : Convert.ToInt32(scalar);
        }
    }

    public async Task<bool> DeleteByIdAsync(int drawId, CancellationToken ct = default)
    {
        var conn = db.Database.GetDbConnection();
        await using (conn)
        {
            if (conn.State != System.Data.ConnectionState.Open)
                await conn.OpenAsync(ct);

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "CALL public.sp_delete_draw(@p_drawid)";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new Npgsql.NpgsqlParameter("p_drawid", drawId));
            await cmd.ExecuteNonQueryAsync(ct);
            return true;
        }
    }

    public async Task<int> UpdateAsync(int old_drawId, Draw candidate, CancellationToken ct = default)
    {
        var conn = db.Database.GetDbConnection();
        await using (conn)
        {
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync(ct);

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT public.fn_update_draw(@p_old_drawid, @p_drawid, @p_n1, @p_n2, @p_n3, @p_n4, @p_n5)";
            cmd.CommandType = CommandType.Text;

            // Parámetros de entrada
            cmd.Parameters.Add(new NpgsqlParameter("p_old_drawid", old_drawId));
            cmd.Parameters.Add(new NpgsqlParameter("p_drawid", candidate.DrawId));
            cmd.Parameters.Add(new NpgsqlParameter("p_n1", candidate.N1));
            cmd.Parameters.Add(new NpgsqlParameter("p_n2", candidate.N2));
            cmd.Parameters.Add(new NpgsqlParameter("p_n3", candidate.N3));
            cmd.Parameters.Add(new NpgsqlParameter("p_n4", candidate.N4));
            cmd.Parameters.Add(new NpgsqlParameter("p_n5", candidate.N5));

            // Ejecuta la función y obtiene el número de filas actualizadas
            var result = await cmd.ExecuteScalarAsync(ct);

            return result == DBNull.Value || result == null ? 0 : Convert.ToInt32(result);
        }
    }

}