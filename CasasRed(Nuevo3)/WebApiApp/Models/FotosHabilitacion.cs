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
    
    public partial class FotosHabilitacion
    {
        public int fh_id { get; set; }
        public string fh_archivo { get; set; }
        public string fh_nombre { get; set; }
        public Nullable<int> fh_habilitacion { get; set; }
    
        public virtual Habilitacion Habilitacion { get; set; }
    }
}
