using System.ComponentModel.DataAnnotations;

namespace SalvandoHuellitas.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Por favor, ingresa tu nombre para continuar.")]
    public string Nombre { get; set; }
    
    [Required(ErrorMessage = "Por favor, ingresa tu número de teléfono para continuar.")]
    [Phone(ErrorMessage = "Por favor, ingresa un número de teléfono válido.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Por favor, ingresa un número de teléfono válido.")]
    public string Telefono { get; set; }

    [Required(ErrorMessage = "Por favor, ingresa tu correo electrónico para continuar.")]
    [EmailAddress(ErrorMessage = "Por favor, ingresa un correo electrónico válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Por favor, ingresa tu contraseña para continuar.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Por favor, confirma tu contraseña para continuar.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden. Por favor, verifica e ingresa las mismas contraseñas.")]
    public string ConfirmPassword { get; set; }
}