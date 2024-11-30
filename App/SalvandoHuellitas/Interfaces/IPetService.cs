using SalvandoHuellitas.Models;

namespace SalvandoHuellitas.Interfaces;

public interface IPetService
{
    Task<List<Mascota>> GetMascotasByUserIdAsync(string userId);
    Task<Mascota?> GetMascotaByIdAsync(int id);
    Task<List<Mascota>> GetMascotasAsync();
    Task<List<ApplicationUser>> GetUsersByPetIdAsync(int petId);
}