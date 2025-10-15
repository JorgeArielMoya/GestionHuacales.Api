using System.ComponentModel.DataAnnotations;

namespace GestionHuacales.Api.Models;

public class EntradasHuacales
{
    [Key]
    public int IdEntrada { get; set; }

    [Required(ErrorMessage = "Campo Requerido")]
    public string NombreCliente { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Cantidad no valida")]
    public int Cantidad { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Precio no valido")]
    public double Precio { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    public virtual ICollection<EntradasHuacalesDetalle> EntradasHuacalesDetalles { get; set; } = [];
}