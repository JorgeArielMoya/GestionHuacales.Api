using GestionHuacales.Api.DTO;
using GestionHuacales.Api.Models;
using GestionHuacales.Api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionHuacales.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoHuacalesController (TipoService tiposService) : ControllerBase
{
    // GET: api/<TipoHuacalesController>
    [HttpGet]
    public async Task <TiposHuacalesDto[]> Get()
    {
        return await tiposService.ListarTiposHuacales(t => true); 
    }

    // GET api/<TipoHuacalesController>/5
    [HttpGet("{id}")]
    public async Task Get(int id)
    {
        await tiposService.ListarTiposHuacales(e => e.TipoId == id);
    }

    // PUT api/<TipoHuacalesController>/5
    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] TiposHuacalesDto tipoDto)
    {
        var tipo = new TiposHuacales
        {
            Descripcion = tipoDto.Descripcion,
        };

        await tiposService.Guardar(tipo);
    }

    // DELETE api/<TipoHuacalesController>/5
    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await tiposService.Eliminar(id);
    }
}
