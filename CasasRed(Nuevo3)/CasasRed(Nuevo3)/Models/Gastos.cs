//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CasasRed_Nuevo3_.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Gastos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gastos()
        {
            this.Contaduria = new HashSet<Contaduria>();
        }
    
        public int Id { get; set; }
        public string Gst_Concepto { get; set; }
        public Nullable<decimal> Gst_Monto { get; set; }
        public Nullable<System.DateTime> Gst_Fecha { get; set; }
        public string Gst_Coment { get; set; }
        public Nullable<int> Id_usuario { get; set; }
        public Nullable<int> Id_Corretaje { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contaduria> Contaduria { get; set; }
        public virtual Corretaje Corretaje { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}