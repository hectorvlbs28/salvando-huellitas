using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalvandoHuellitas.Models;
using SalvandoHuellitas.ViewModels;
using SalvandoHuellitas.Enums;

namespace SalvandoHuellitas.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Register()
    {
        return View();
    }
    
    private string TraducirMensajeError(string mensaje)
    {
        return mensaje switch
        {
            "Passwords must be at least 6 characters." => "Las contraseñas deben tener al menos 6 caracteres.",
            "Passwords must have at least one non alphanumeric character." => "Las contraseñas deben tener al menos un carácter no alfanumérico.",
            "Passwords must have at least one digit ('0'-'9')." => "Las contraseñas deben tener al menos un dígito ('0'-'9').",
            "Passwords must have at least one lowercase ('a'-'z')." => "Las contraseñas deben tener al menos una letra minúscula ('a'-'z').",
            "Passwords must have at least one uppercase ('A'-'Z')." => "Las contraseñas deben tener al menos una letra mayúscula ('A'-'Z').",
            _ => mensaje
        };
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Nombre = model.Nombre,
                Telefono = model.Telefono
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                TempData["SuccessMessage"] = ToastMessages._AccountRegisterSuccess;
                return RedirectToAction("Index", "Home");
            }

            if (result.Errors.Any(e => e.Code == "DuplicateUserName"))
            {
                TempData["ErrorMessage"] = ToastMessages._AccountRegisterEmailExists;
            }
            else if (result.Errors.Any(e => e.Code.StartsWith("Password")))
            {
                var passwordErrors = result.Errors
                    .Where(e => e.Code.StartsWith("Password"))
                    .Select(e => e.Description);
                TempData["ErrorMessage"] = string.Join(" ", passwordErrors.Select(e => TraducirMensajeError(e)));
            }
            else
            {
                TempData["ErrorMessage"] = ToastMessages._AccountRegisterFailed;
            }
        }
        return View(model);
    }
    
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> LoginService(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = ToastMessages._AccountLoginSuccess;
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = ToastMessages._AccountLoginFailed;
        }
        return View(model);
    }
    
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}