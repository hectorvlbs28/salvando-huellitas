using Microsoft.AspNetCore.Identity;

namespace SalvandoHuellitas.Models;

public class ApplicationUser : IdentityUser
{
    public string Nombre { get; set; }
    
    public string Telefono { get; set; }
}