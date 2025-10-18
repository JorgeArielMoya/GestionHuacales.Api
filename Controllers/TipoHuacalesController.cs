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
    public async Task<ActionResult<TiposHuacalesDto>> Get(int id)
    {
        var tipos = await tiposService.ListarTiposHuacales(e => e.TipoId == id);

        if (tipos == null || tipos.Length == 0)
        {
            return NotFound($"No se encontró ningún tipo de huacal con el ID {id}");
        }

        return Ok(tipos.First());
    }
}
