using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parcial1_PrograIX.Models
{
    public class ValidacionesPiloto
    {

    [Required(ErrorMessage = "Ingrese un tipo de licencia")]
    [DataType(DataType.Text)]
    [MaxLength(1)]
    [RegularExpression(@"^[abcmABCM]{1}$", ErrorMessage = "Solo son permitidos las licencias tipo A,B,C,M")]
    public string licencia { get; set; }

    [Required(ErrorMessage = "Ingrese su genero")]
    [DataType(DataType.Text)]
    [MaxLength(6)]
    [RegularExpression(@"\b(mujer|hombre)\b", ErrorMessage = "Solo son permitidos los generos hombre y mujer")]
    public string genero { get; set; }

    [Required(ErrorMessage = "Ingrese un email correcto")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Dirección de Email")]
    [MaxLength(50)]
    [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Ingrese un email correcto")]
    [Remote("IsUserInDB", "PilotoesApiController", HttpMethod = "POST", ErrorMessage = "Existe correo registrado.")]
    //[Remote("ExisteoPilocoConCorreo", "PilotoesApiController", ErrorMessage = "Existe correo registrado")]
    public string correo { get; set; }

    [Required]
    public string nombres { get; set; }
    [Required]
    public string apellidos { get; set; }
    [Required]
    public string telefono { get; set; }
    [Required]
    public string direccion { get; set; }
    [Required]
    public System.DateTime nacimiento { get; set; }
    [Required]
    public string estado { get; set; }
        
    }
}