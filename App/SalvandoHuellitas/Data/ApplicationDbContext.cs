using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalvandoHuellitas.Models;

namespace SalvandoHuellitas.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    
    public DbSet<Mascota?> Mascotas { get; set; }
    public DbSet<Foto> Fotos { get; set; }
    public DbSet<SolicitudAdopcion> SolicitudesAdopcion { get; set; }
}