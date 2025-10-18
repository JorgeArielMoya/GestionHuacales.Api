using GestionHuacales.Api.DAL;
using GestionHuacales.Api.DTO;
using GestionHuacales.Api.Models;
using GestionHuacales.Api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionHuacales.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntradasHuacalesController (EntradasService entradasService) : ControllerBase
{
    // GET: api/<EntradasHuacalesController>
    [HttpGet]
    public async Task<EntradasHuacalesDto[]> Get()
    {
        return await entradasService.Listar(e => true);
    }

    // GET api/<EntradasHuacalesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EntradasHuacalesDto>> Get(int id)
    {
        var entrada = await entradasService.Buscar(id);

        if (entrada == null)
        {
            return NotFound($"No se encontró una entrada con el ID {id}");
        }

        var dto = new EntradasHuacalesDto
        {
            NombreCliente = entrada.NombreCliente,
            EntradasHuacalesDetallesDto = entrada.EntradasHuacalesDetalles
                .Select(d => new EntradasHuacalesDetallesDto
                {
                    TipoId = d.TipoId,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio,
                })
                .ToArray()
        };

        return Ok(dto);
    }

    // POST api/<EntradasHuacalesController>
    [HttpPost]
    public async Task Post([FromBody] EntradasHuacalesDto entradasHuacales)
    {
        var huacal = new EntradasHuacales
        {
            Fecha = DateTime.Now,
            NombreCliente = entradasHuacales.NombreCliente, 
            EntradasHuacalesDetalles = entradasHuacales.EntradasHuacalesDetallesDto.Select (e => new EntradasHuacalesDetalle
            {
                TipoId = e.TipoId, 
                Cantidad = e.Cantidad,
                Precio = e.Precio,
            }).ToArray()
        };

        await entradasService.Guardar(huacal);  
    }

    // PUT api/<EntradasHuacalesController>/5
    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] EntradasHuacalesDto entradaDto)
    {
        var huacal = new EntradasHuacales
        {
            Fecha = DateTime.Now,
            IdEntrada = id,
            NombreCliente = entradaDto.NombreCliente,
            EntradasHuacalesDetalles = entradaDto.EntradasHuacalesDetallesDto.Select(d => new EntradasHuacalesDetalle
            {
                TipoId = d.TipoId,
                Cantidad = d.Cantidad,
                Precio = d.Precio,
            }).ToArray()
        };
        await entradasService.Guardar (huacal);
    }

    // DELETE api/<EntradasHuacalesController>/5
    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await entradasService.Eliminar(id); 
    }
}
