using System.ComponentModel.DataAnnotations;

namespace SalvandoHuellitas.ViewModels;

public class NewPetViewModel
{
    [Required(ErrorMessage = "Por favor, ingresa el nombre de tu mascota para continuar.")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Por favor, selecciona el tipo de mascota para continuar.")]
    public string Tipo { get; set; }

    [Required(ErrorMessage = "La descripción de la mascota es obligatoria.")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "Por favor, selecciona el género de tu mascota para continuar.")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "Por favor, ingresa la edad de tu mascota para continuar.")]
    public int Edad { get; set; }
    
    [Required(ErrorMessage = "Por favor, selecciona el tamaño de tu mascota para continuar.")]
    public string Tamaño { get; set; }
    
    [Required(ErrorMessage = "Por favor, selecciona la sociabilidad de tu mascota para continuar.")]
    public string SociableCon { get; set; }
    
    [Required(ErrorMessage = "Por favor, ingresa la localización de tu mascota para continuar.")]
    public string Localizacion { get; set; }
    
    [Required(ErrorMessage = "Por favor, ingresa los requisitos de adopción de tu mascota para continuar.")]
    public string RequisitosAdopcion { get; set; }
    
    [Required(ErrorMessage = "Por favor, selecciona una foto de tu mascota para continuar.")]
    [DataType(DataType.Upload)]
    public IFormFile Foto { get; set; }
}