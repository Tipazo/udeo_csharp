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
    
    public partial class Asignacion_jugadores
    {
        public int Id { get; set; }
        public int jugador_id { get; set; }
        public int equipo_id { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
        public int numero_camisola { get; set; }
        public string posicion { get; set; }
    
        public virtual Jugadore Jugadore { get; set; }
        public virtual Equipos Equipos { get; set; }
    }
}