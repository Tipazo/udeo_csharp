using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parcial1_PrograIX.Models
{
    public class ValidacionesMarcas
    {

        [Required(ErrorMessage = "Ingrese la marca del vehiculo")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [Remote("IsmarcaInDB", "MarcasApi", HttpMethod = "POST", ErrorMessage = "Existe correo registrado.")]
        public string nombre { get; set; }

        [Required]
        public string estado { get; set; }

        [Required]
        public System.DateTime created_at { get; set; }

        [Required]
        public System.DateTime updated_at { get; set; }
    }
}