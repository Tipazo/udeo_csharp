//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Parcial1_PrograIX.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Percance
    {
        public int Id { get; set; }
        public int vehiculo_id { get; set; }
        public int piloto_id { get; set; }
        public int tarifa_id { get; set; }
        public int anio_percance { get; set; }
        public int anio_vehiculo { get; set; }
        public decimal total_deducible { get; set; }
        public string estado { get; set; }
        public System.DateTime created_at { get; set; }
        public decimal costo_vehiculo { get; set; }
        public string json_resultado { get; set; }
    
        public virtual Piloto Piloto { get; set; }
        public virtual Vehiculo Vehiculo { get; set; }
        public virtual Tarifa Tarifa { get; set; }
    }
}