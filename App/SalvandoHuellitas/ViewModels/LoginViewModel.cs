using System.ComponentModel.DataAnnotations;

namespace SalvandoHuellitas.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Por favor, ingresa tu correo electrónico para continuar.")]
    [EmailAddress(ErrorMessage = "Por favor, ingresa un correo electrónico válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Por favor, ingresa tu contraseña para continuar.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}