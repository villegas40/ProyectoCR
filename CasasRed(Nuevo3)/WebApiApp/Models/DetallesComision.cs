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
    
    public partial class DetallesComision
    {
        public int Id { get; set; }
        public Nullable<int> Id_Vendedor { get; set; }
        public Nullable<int> Id_Comision { get; set; }
        public Nullable<decimal> DCms_Monto { get; set; }
        public string DCms_TipoCom { get; set; }
    }
}
