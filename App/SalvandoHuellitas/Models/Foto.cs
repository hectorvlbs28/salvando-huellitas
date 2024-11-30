using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalvandoHuellitas.Models;

public class Foto
{
    public int Id { get; set; }

    [Required]
    public string Url { get; set; }

    [ForeignKey("Mascota")]
    public int MascotaId { get; set; }
    public Mascota Mascota { get; set; }
}