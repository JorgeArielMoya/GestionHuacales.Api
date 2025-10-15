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
    public string Get(int id)
    {
        return "value";
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
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<EntradasHuacalesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
