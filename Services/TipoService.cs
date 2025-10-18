using System.Linq.Expressions;
using GestionHuacales.Api.DAL;
using GestionHuacales.Api.DTO;
using GestionHuacales.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionHuacales.Api.Services;

public class TipoService(IDbContextFactory<Contexto> DbFactory)
{
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
