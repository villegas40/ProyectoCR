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
    
    public partial class CasaInventario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CasaInventario()
        {
            this.HistorialAsignacion = new HashSet<HistorialAsignacion>();
        }
    
        public int ci_Id { get; set; }
        public int ci_corretaje_id { get; set; }
        public decimal ci_cantidadAsignada { get; set; }
        public System.DateTime ci_fecha { get; set; }
        public int ci_usuario_id { get; set; }
        public string ci_articulo_id { get; set; }
    
        public virtual Articulos Articulos { get; set; }
        public virtual Corretaje Corretaje { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistorialAsignacion> HistorialAsignacion { get; set; }
    }
}
