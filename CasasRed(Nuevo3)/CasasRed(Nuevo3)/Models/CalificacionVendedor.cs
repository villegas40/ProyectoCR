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
    
    public partial class CalificacionVendedor
    {
        public int Id { get; set; }
        public Nullable<decimal> DVndr_Puntaje { get; set; }
        public Nullable<int> Id_Corretaje { get; set; }
        public Nullable<int> Id_Vendedor { get; set; }
    
        public virtual Corretaje Corretaje { get; set; }
        public virtual Vendedor Vendedor { get; set; }
    }
}
