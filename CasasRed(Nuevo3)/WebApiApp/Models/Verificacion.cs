//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Verificacion
    {
        public int Id { get; set; }
        public Nullable<bool> Vfn_Persona_fisica { get; set; }
        public Nullable<bool> Vfn_Visto_persona { get; set; }
        public Nullable<bool> Vfn_Tiempo_estimado { get; set; }
        public string Vfn_Tiempo { get; set; }
        public Nullable<bool> Vfn_Tiene_costo { get; set; }
        public Nullable<decimal> Vfn_Costo { get; set; }
        public Nullable<int> Vfn_Trato_asesor { get; set; }
        public string Vfn_Observaciones { get; set; }
        public Nullable<int> Id_Cliente { get; set; }
    
        public virtual Cliente Cliente { get; set; }
    }
}
