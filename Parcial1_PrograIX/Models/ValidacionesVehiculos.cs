using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parcial1_PrograIX.Models
{
    public class ValidacionesVehiculos
    {

        [Required(ErrorMessage = "La placa debe tener 7 caracteres")]
        [StringLength(7)]
        public string placa { get; set; }
        [Required]
        public string tipo { get; set; }
        [Required]
        public int marca_id { get; set; }
        [Required]
        public string modelo { get; set; }
        [Required]
        public int anio { get; set; }
        [Required]
        public int motor { get; set; }
        [Required]
        public int gasolina { get; set; }
        [Required]
        public int odometro { get; set; }
        [Required]
        public string color { get; set; }
        [Required]
        public int costo_inicial { get; set; }
        [Required]
        public int costo_variacion { get; set; }
        [Required]
        public string estado { get; set; }
        [Required]
        public System.DateTime created_at { get; set; }
        [Required]
        public System.DateTime updated_at { get; set; }
        [Required]
        public int llantas { get; set; }
        [Required]
        public int puertas { get; set; }
    }
}