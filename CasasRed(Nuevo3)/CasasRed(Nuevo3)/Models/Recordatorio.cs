//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CasasRed_Nuevo3_.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recordatorio
    {
        public int Rcd_Id { get; set; }
        public string Rcd_Descripción { get; set; }
        public Nullable<System.DateTime> Rcd_FechaAlta { get; set; }
        public Nullable<int> Rcd_Id_Usuario { get; set; }
        public Nullable<int> Rcd_Id_Gestion { get; set; }
    
        public virtual Gestion Gestion { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
