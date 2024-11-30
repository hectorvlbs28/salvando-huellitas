using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalvandoHuellitas.Models;
using SalvandoHuellitas.ViewModels;
using SalvandoHuellitas.Data;
using SalvandoHuellitas.Enums;
using SalvandoHuellitas.Interfaces;

namespace SalvandoHuellitas.Controllers;

public class PetsController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly IPetService _petService;
    
    public PetsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IPetService petService)
    {
        _userManager = userManager;
        _context = context;
        _petService = petService;
    }
    
    [HttpGet]
    public IActionResult NewPet()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SaveNewPetService(NewPetViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("NewPet");
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["ErrorMessage"] = ToastMessages._PetsLoginRequired;
            return RedirectToAction("Login", "Account");
        }

        var newPet = new Mascota
        {
            Nombre = model.Nombre,
            Tipo = model.Tipo,
            Descripcion = model.Descripcion,
            Genero = model.Genero,
            Edad = model.Edad,
            Tamaño = model.Tamaño,
            SociableCon = model.SociableCon,
            Localizacion = model.Localizacion,
            RequisitosAdopcion = model.RequisitosAdopcion,
            Adoptado = false,
            FechaRegistro = DateTime.Now,
            UsuarioId = user.Id
        };

        _context.Mascotas.Add(newPet);
        await _context.SaveChangesAsync();

        if (model.Foto.Length > 0)
        {
            var fileExtension = Path.GetExtension(model.Foto.FileName);
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine("wwwroot/images/pets/", fileName);
            
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.Foto.CopyToAsync(stream);
            }
            
            var fotoUrl = $"/images/pets/{fileName}";

            var newFoto = new Foto
            {
                Url = fotoUrl,
                MascotaId = newPet.Id
            };

            _context.Fotos.Add(newFoto);
            await _context.SaveChangesAsync();
        }

        TempData["SuccessMessage"] = ToastMessages._PetsSuccessMessage;
        return RedirectToAction("Index", "Home"); 
    }
    
    public async Task<IActionResult> EditPet(int id)
    {
        var pet = await _petService.GetMascotaByIdAsync(id);
        
        if (pet == null)
        {
            TempData["ErrorMessage"] = ToastMessages._PetsPetNotFound;
            return NotFound();
        }
        
        var users = await _petService.GetUsersByPetIdAsync(id);
        
        var model = new PetEditViewModel
        {
            Id = pet.Id,
            Nombre = pet.Nombre,
            Tipo = pet.Tipo,
            Descripcion = pet.Descripcion,
            Genero = pet.Genero,
            Edad = pet.Edad,
            Tamaño = pet.Tamaño,
            SociableCon = pet.SociableCon,
            Localizacion = pet.Localizacion,
            RequisitosAdopcion = pet.RequisitosAdopcion,
            Adoptado = pet.Adoptado,
            Usuarios = users
        };
        
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdatePet(PetEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("EditPet", model);
        }
        
        var pet = await _petService.GetMascotaByIdAsync(model.Id);
        
        if (pet == null)
        {
            TempData["ErrorMessage"] = ToastMessages._PetsPetNotFound;
            return View("EditPet", model);
        }
        
        pet.Nombre = model.Nombre;
        pet.Tipo = model.Tipo;
        pet.Descripcion = model.Descripcion;
        pet.Genero = model.Genero;
        pet.Edad = model.Edad;
        pet.Tamaño = model.Tamaño;
        pet.SociableCon = model.SociableCon;
        pet.Localizacion = model.Localizacion;
        pet.RequisitosAdopcion = model.RequisitosAdopcion;
        pet.Adoptado = model.Adoptado;
        
        _context.Mascotas.Update(pet);
        await _context.SaveChangesAsync();
        
        TempData["SuccessMessage"] = ToastMessages._PetsUpdatePetSuccess;
        return RedirectToAction("UserIndex", "UserInformation");
    }
    
    public async Task<IActionResult> ListPets()
    {
        var pets = await _petService.GetMascotasAsync();
        
        return View(pets);
    }
    
    public async Task<IActionResult> PetDetails(int id)
    {
        var pet = await _petService.GetMascotaByIdAsync(id);
    
        if (pet == null)
        {
            TempData["ErrorMessage"] = ToastMessages._PetsPetNotFound;
            return NotFound();
        }
    
        var model = new PetDetailsViewModel
        {
            Id = pet.Id,
            Nombre = pet.Nombre,
            Tipo = pet.Tipo,
            Descripcion = pet.Descripcion,
            Genero = pet.Genero,
            Edad = pet.Edad,
            Tamaño = pet.Tamaño,
            SociableCon = pet.SociableCon,
            Localizacion = pet.Localizacion,
            RequisitosAdopcion = pet.RequisitosAdopcion,
            UsuarioId = pet.UsuarioId,
            FotoUrl = pet.Fotos.FirstOrDefault()?.Url
        };
    
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> AdoptPet(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["ErrorMessage"] = ToastMessages._PetsAdoptLoginRequired;
            return RedirectToAction("Login", "Account");
        }

        var pet = await _context.Mascotas.FindAsync(id);
        if (pet == null)
        {
            TempData["ErrorMessage"] = ToastMessages._PetsPetNotFound;
            return NotFound();
        }

        if (pet.UsuarioId == user.Id)
        {
            TempData["ErrorMessage"] = ToastMessages._PetsUserHasThisPet;
            return RedirectToAction("PetDetails", new { id });
        }
        
        var existingSolicitud = await _context.SolicitudesAdopcion
            .FirstOrDefaultAsync(s => s.MascotaId == id && s.UsuarioId == user.Id);
        if (existingSolicitud != null)
        {
            TempData["ErrorMessage"] = ToastMessages._PetsAdoptExisting;
            return RedirectToAction("PetDetails", new { id });
        }

        var solicitudAdopcion = new SolicitudAdopcion
        {
            MascotaId = id,
            UsuarioId = user.Id,
            Fecha = DateTime.Now
        };

        _context.SolicitudesAdopcion.Add(solicitudAdopcion);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = ToastMessages._PetsAdoptSucces;
        return RedirectToAction("PetDetails", new { id });
    }
}