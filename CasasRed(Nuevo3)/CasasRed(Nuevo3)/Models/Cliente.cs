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
    
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            this.Gestion = new HashSet<Gestion>();
        }
    
        public int Id { get; set; }
        public string Gral_Nombre { get; set; }
        public string Gral_Apellidopa { get; set; }
        public string Gral_Apellidoma { get; set; }
        public Nullable<System.DateTime> Gral_Fechanac { get; set; }
        public string Gral_Nss { get; set; }
        public string Gral_Curp { get; set; }
        public string Gral_Rfc { get; set; }
        public string Gral_Lugarnac { get; set; }
        public string Gral_Calle { get; set; }
        public string Gral_Numero { get; set; }
        public string Gral_Cp { get; set; }
        public string Gral_Colonia { get; set; }
        public string Gral_Municipio { get; set; }
        public string Gral_Estado { get; set; }
        public string Gral_Celular { get; set; }
        public string Gral_Tel_casa { get; set; }
        public string Gral_Estado_civil { get; set; }
        public string Gral_Regimen_matrimonial { get; set; }
        public string Gral_Ocupacion { get; set; }
        public string Gral_Teltrabajo { get; set; }
        public string Gral_Correo { get; set; }
        public string Gral_Identificacion { get; set; }
        public Nullable<int> Gral_No_identificacion { get; set; }
        public string Gral_Ref_nombre1 { get; set; }
        public string Gral_Ref_cel_1 { get; set; }
        public string Gral_Ref_nombre2 { get; set; }
        public string Gral_Ref_cel_2 { get; set; }
        public string Cyg_Nombre { get; set; }
        public string Cyg_Apellidopa { get; set; }
        public string Cyg_Apellidoma { get; set; }
        public Nullable<System.DateTime> Gyg_Fechanac { get; set; }
        public string Cyg_Nss { get; set; }
        public string Cyg_Curp { get; set; }
        public string Cyg_Rfc { get; set; }
        public string Cyg_Lugarnac { get; set; }
        public string Cyg_Celular { get; set; }
        public string Cyg_Tel_casa { get; set; }
        public string Cyg_Ocupacion { get; set; }
        public string Cyg_Tel_trabajo { get; set; }
        public string Cyg_Correo { get; set; }
        public string Cyg_Identificacion { get; set; }
        public Nullable<int> Cyg_No_identificacoion { get; set; }
        public Nullable<System.DateTime> Gral_Fechaalta { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gestion> Gestion { get; set; }
    }
}
