using Microsoft.EntityFrameworkCore;
using SalvandoHuellitas.Data;
using SalvandoHuellitas.Interfaces;
using SalvandoHuellitas.Models;

namespace SalvandoHuellitas.Services;

public class PetService : IPetService
{
    private readonly ApplicationDbContext _context;

    public PetService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Mascota>> GetMascotasByUserIdAsync(string userId)
    {
        return await _context.Mascotas
            .Where(m => m.UsuarioId == userId)
            .Include(m => m.Fotos)
            .OrderByDescending(m => m.FechaRegistro)
            .ToListAsync();
    }

    public async Task<Mascota?> GetMascotaByIdAsync(int id)
    {
        return await _context.Mascotas
            .Include(p => p.Usuario)
            .Include(p => p.Fotos)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<List<Mascota>> GetMascotasAsync()
    {
        return await _context.Mascotas
            .Where(m => m.Adoptado == false)
            .Include(m => m.Fotos)
            .OrderByDescending(m => m.FechaRegistro)
            .ToListAsync();
    }
    
    public async Task<List<ApplicationUser>> GetUsersByPetIdAsync(int petId)
    {
        var users = await _context.SolicitudesAdopcion
            .Where(s => s.MascotaId == petId)
            .Include(s => s.Usuario)
            .Select(s => s.Usuario)
            .ToListAsync();

        return users;
    }
}