using System.Linq.Expressions;
using GestionHuacales.Api.DAL;
using GestionHuacales.Api.DTO;
using GestionHuacales.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionHuacales.Api.Services;

public class TipoService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar (TiposHuacales tipoHuacal)
    {
        if (!await Existe (tipoHuacal.TipoId))
        {
            return await Insertar (tipoHuacal); 
        }
        else
        {
            return await Modificar (tipoHuacal);
        }
    }

    public async Task<bool> Existe (int tipoId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.TiposHuacales
            .AnyAsync(t => t.TipoId == tipoId);  
    }

    public async Task<bool> Insertar (TiposHuacales tipoHuacal)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        Contexto.TiposHuacales.Add (tipoHuacal);
        return await Contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Modificar (TiposHuacales tipoHuacal)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        Contexto.Update(tipoHuacal);
        return await Contexto.SaveChangesAsync() > 0;
    }
    public async Task<TiposHuacales?> Buscar(int tipoId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.TiposHuacales.FirstOrDefaultAsync(t => t.TipoId == tipoId);
    }
    public async Task<bool> Eliminar (int tipoId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.TiposHuacales
            .AsNoTracking()
            .Where(e => e.TipoId == tipoId)
            .ExecuteDeleteAsync() > 0;
    }
    public async Task<TiposHuacalesDto[]> ListarTiposHuacales(Expression<Func<TiposHuacales, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposHuacales.Where(criterio).Select(t => new TiposHuacalesDto
        {
            TipoId = t.TipoId,
            Descripcion = t.Descripcion,
            Existencia = t.Existencia
        }).ToArrayAsync();
    }
}
