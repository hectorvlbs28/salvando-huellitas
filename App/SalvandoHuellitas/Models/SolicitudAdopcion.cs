using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalvandoHuellitas.Models;

public class SolicitudAdopcion
{
    public int Id { get; set; }

    [ForeignKey("Mascota")]
    public int MascotaId { get; set; }
    public Mascota Mascota { get; set; }

    [ForeignKey("Usuario")]
    public string UsuarioId { get; set; }
    public ApplicationUser Usuario { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;
}