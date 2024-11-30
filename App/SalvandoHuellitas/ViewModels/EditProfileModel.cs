using System.ComponentModel.DataAnnotations;

namespace SalvandoHuellitas.ViewModels;

public class EditProfileModel
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; }
        
    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "El teléfono no es válido.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener 10 dígitos. Por favor, verifica el número ingresado.")]
    public string Telefono { get; set; }
}