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
    
    public partial class Registros_empleados
    {
        public string tipo { get; set; }
        public int estudiante_id { get; set; }
        public System.DateTime fecha { get; set; }
        public System.DateTime hora_registro { get; set; }
    
        public virtual Estudiante Estudiante { get; set; }
    }
}
