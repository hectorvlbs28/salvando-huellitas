using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalvandoHuellitas.Models;

public class Mascota
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; }

    [Required]
    public string Tipo { get; set; }

    [Required]
    public string Descripcion { get; set; }

    [Required]
    public string Genero { get; set; }

    [Required]
    public int Edad { get; set; }
    
    [Required]
    public string Tamaño { get; set; }
    
    [Required]
    public string SociableCon { get; set; }
    
    [Required]
    public string Localizacion { get; set; }
    
    [Required]
    public string RequisitosAdopcion { get; set; }
    
    [Required]
    public Boolean Adoptado { get; set; }
    
    [Required]
    public DateTime FechaRegistro { get; set; }

    [ForeignKey("Usuario")]
    public string UsuarioId { get; set; }
    public ApplicationUser Usuario { get; set; }

    public ICollection<Foto> Fotos { get; set; }
}