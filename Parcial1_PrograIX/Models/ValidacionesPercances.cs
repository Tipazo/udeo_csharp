using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Parcial1_PrograIX.Models
{
    public class ValidacionesPercances
    {

        public int Id { get; set; }
        [Required]
        public int vehiculo_id { get; set; }
        [Required]
        public int piloto_id { get; set; }
        [Required]
        public int tarifa_id { get; set; }
        [Required]
        public int anio_percance { get; set; }
        [Required]
        public int anio_vehiculo { get; set; }
        [Required]
        public decimal total_deducible { get; set; }
        [Required]
        public string estado { get; set; }
        [Required]
        public System.DateTime created_at { get; set; }
        [Required]
        public decimal costo_vehiculo { get; set; }
        [Required]
        public string json_resultado { get; set; }
    }
}