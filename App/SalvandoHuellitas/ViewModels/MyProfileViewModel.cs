using SalvandoHuellitas.Models;

namespace SalvandoHuellitas.ViewModels;

public class MyProfileViewModel
{
    public string? Email { get; set; }
    public string Nombre { get; set; }
    
    public string NombreAsociacion { get; set; }
    public string Telefono { get; set; }
    
    public List<Mascota> Mascotas { get; set; }
}