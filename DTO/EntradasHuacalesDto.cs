namespace GestionHuacales.Api.DTO;

public class EntradasHuacalesDto
{
    public string NombreCliente { get; set; }
    public EntradasHuacalesDetallesDto[] EntradasHuacalesDetallesDto { get; set; } = [];
}
