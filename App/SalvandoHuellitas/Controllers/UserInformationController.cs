using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalvandoHuellitas.Models;
using SalvandoHuellitas.ViewModels;
using SalvandoHuellitas.Enums;
using SalvandoHuellitas.Data;
using SalvandoHuellitas.Interfaces;


namespace SalvandoHuellitas.Controllers;

public class UserInformationController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly IPetService _petService;

    
    public UserInformationController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IPetService petService)
    {
        _userManager = userManager;
        _context = context;
        _petService = petService;
    }
    
    public async Task<IActionResult> UserIndex()
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user == null)
        {
            TempData["ErrorMessage"] = ToastMessages._UserInformationIndexLoginRequired;
            return RedirectToAction("Login", "Account");
        }
        
        var mascotas = await _petService.GetMascotasByUserIdAsync(user.Id);
        
        var model = new MyProfileViewModel()
        {
            Email = user.Email,
            Nombre = user.Nombre,
            Telefono = user.Telefono,
            Mascotas = mascotas
        };
        
        return View(model);
    }
    
    public async Task<IActionResult> EditProfile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["ErrorMessage"] = ToastMessages._UserInformationEditProfileLoginRequired;
            return RedirectToAction("Login", "Account");
        }

        var model = new EditProfileModel()
        {
            Nombre = user.Nombre,
            Telefono = user.Telefono
        };

        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateProfile(EditProfileModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["ErrorMessage"] = ToastMessages._UserInformationEditProfileLoginRequired;
            return RedirectToAction("Login", "Account");
        }

        user.Nombre = model.Nombre;
        user.Telefono = model.Telefono;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = ToastMessages._UserInformationUpdateProfileSuccess;
            return RedirectToAction("UserIndex");
        }

        return View("EditProfile", model);
    }
}