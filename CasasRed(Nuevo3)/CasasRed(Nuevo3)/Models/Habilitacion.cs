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
    
    public partial class Habilitacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Habilitacion()
        {
            this.FotosHabilitacion = new HashSet<FotosHabilitacion>();
        }
    
        public int Id { get; set; }
        public Nullable<bool> Hbt_Puertas { get; set; }
        public Nullable<bool> Hbt_Chapas { get; set; }
        public Nullable<bool> Hbt_Marcos_puertas { get; set; }
        public Nullable<bool> Hbt_Bisagras { get; set; }
        public Nullable<bool> Hbt_Taza { get; set; }
        public Nullable<bool> Hbt_Lavamanos { get; set; }
        public Nullable<bool> Hbt_Bastago { get; set; }
        public Nullable<bool> Hbt_Chapeton { get; set; }
        public Nullable<bool> Hbt_Maneral { get; set; }
        public Nullable<bool> Hbt_Regadera_completa { get; set; }
        public Nullable<bool> Hbt_Kit_lavamanos { get; set; }
        public Nullable<bool> Hbt_Kit_taza { get; set; }
        public Nullable<bool> Hbt_Rosetas { get; set; }
        public Nullable<bool> Hbt_Apagador_sencillo { get; set; }
        public Nullable<bool> Hbt_Conector_sencillo { get; set; }
        public Nullable<bool> Hbt_Apagador_doble { get; set; }
        public Nullable<bool> Hbt_Conector_apagador { get; set; }
        public Nullable<bool> Hbt_Domo { get; set; }
        public Nullable<bool> Hbt_Ventanas { get; set; }
        public Nullable<bool> Hbt_Cableado { get; set; }
        public string Hbt_Calibre_cableado { get; set; }
        public Nullable<bool> Hbt_Break_interior { get; set; }
        public Nullable<bool> Hbt_Break_medidor { get; set; }
        public Nullable<bool> Hbt_Pinturas { get; set; }
        public Nullable<bool> Hbt_AvisoSusp { get; set; }
        public Nullable<int> Hbt_ProgresoForm { get; set; }
        public Nullable<int> Id_Corretaje { get; set; }
        public Nullable<int> Id_Usuario { get; set; }
        public string Hbt_StatusCasa { get; set; }
        public Nullable<System.DateTime> Hbt_FchEntrega { get; set; }
    
        public virtual Corretaje Corretaje { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FotosHabilitacion> FotosHabilitacion { get; set; }
    }
}
