using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Parcial1_PrograIX.Models
{
    public class ValidacionesTarifa
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public string json_tarifas { get; set; }
        [Required]
        public string estado { get; set; }
        [Required]
        public System.DateTime created_at { get; set; }
        [Required]
        public System.DateTime updated_at { get; set; }
    }
}