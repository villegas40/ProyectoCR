//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Articulos
    {
        public string art_id { get; set; }
        public string art_nombre { get; set; }
        public string art_descripcion { get; set; }
        public Nullable<System.DateTime> art_fechaIngreso { get; set; }
        public Nullable<decimal> art_cantidadMinima { get; set; }
    }
}
