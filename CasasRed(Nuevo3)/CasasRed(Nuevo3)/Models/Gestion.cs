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
    
    public partial class Gestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gestion()
        {
            this.Recordatorio = new HashSet<Recordatorio>();
        }
    
        public int Id { get; set; }
        public Nullable<bool> Gtn_Escrituras { get; set; }
        public Nullable<bool> Gtn_Planta_Cartografica { get; set; }
        public Nullable<bool> Gtn_Predial { get; set; }
        public Nullable<bool> Gtn_Recibo_Luz { get; set; }
        public Nullable<bool> Gtn_Recibo_Agua { get; set; }
        public Nullable<bool> Gtn_Acta_Nacimiento { get; set; }
        public Nullable<bool> Gtn_IFE_Copia { get; set; }
        public Nullable<bool> Gtn_Sol_Ret_Ifo { get; set; }
        public Nullable<bool> Gtn_Cert_Hip { get; set; }
        public Nullable<bool> Gtn_Cert_Fiscal { get; set; }
        public Nullable<bool> Gtn_Sol_Estado { get; set; }
        public Nullable<bool> Gtn_Junta_URBI { get; set; }
        public Nullable<bool> Gtn_Agua_Pago_Inf { get; set; }
        public Nullable<bool> Gtn_Cert_Cartogr { get; set; }
        public Nullable<bool> Gtn_No_Oficial { get; set; }
        public Nullable<bool> Gtn_Avaluo { get; set; }
        public Nullable<bool> Gtn_CT_Banco { get; set; }
        public Nullable<bool> Gtn_Aviso_Suspension { get; set; }
        public Nullable<bool> Gtn_Formato_Infonavit { get; set; }
        public Nullable<bool> Gtn_Fotos_Propiedad { get; set; }
        public Nullable<bool> Gtn_Evaluo_Contacto { get; set; }
        public Nullable<bool> Gtn_Planeacion_Fianza { get; set; }
        public Nullable<bool> Gtn_Urbanizacion { get; set; }
        public Nullable<bool> Gtn_Credito_INFONAVIT { get; set; }
        public Nullable<bool> Gtn_Notaria { get; set; }
        public Nullable<bool> Gtn_Firma_Escrituras { get; set; }
        public Nullable<bool> Gtn_CuentaBancaria { get; set; }
        public Nullable<bool> Gtn_Taller { get; set; }
        public Nullable<int> Gtn_ProgresoForm { get; set; }
        public Nullable<int> Id_Corretaje { get; set; }
        public Nullable<int> Id_Cliente { get; set; }
        public Nullable<int> Id_Usuario { get; set; }
        public Nullable<bool> Gtn_ReciboActualizado { get; set; }
        public Nullable<System.DateTime> Gtn_FechaAlta { get; set; }
        public Nullable<bool> Gtn_Aviso_Ret { get; set; }
        public Nullable<bool> Gtn_Acta_Nacim_Cony { get; set; }
        public Nullable<bool> Gtn_Acta_Matrimonio { get; set; }
        public Nullable<bool> Gtn_DatosGnrl_Comp { get; set; }
        public Nullable<bool> Gtn_Comp_Domicilio { get; set; }
        public Nullable<bool> Gtn_Recibo_Nomina { get; set; }
        public Nullable<bool> Gtn_RFC_Comprador { get; set; }
        public Nullable<bool> Gtn_CURP_Comprador { get; set; }
        public Nullable<bool> Gtn_RFC_Conyugue { get; set; }
        public Nullable<bool> Gtn_CURP_Conyugue { get; set; }
        public Nullable<bool> Gtn_Inscp_INFONAVIT { get; set; }
        public Nullable<bool> Gtn_Acta_Nac_Ven { get; set; }
        public Nullable<bool> Gtn_Acta_Nac_Cony_Ven { get; set; }
        public Nullable<bool> Gtn_Acta_Matrimonio_Ven { get; set; }
        public Nullable<bool> Gtn_IFE_Copia_Ven { get; set; }
        public Nullable<bool> Gtn_RFC_Ven { get; set; }
        public Nullable<bool> Gtn_CURP_Ven { get; set; }
        public Nullable<bool> Gtn_RFC_Conyu_Ven { get; set; }
        public Nullable<bool> Gtn_CURP_Conyu_Ven { get; set; }
        public Nullable<bool> Gtn_Entrega_Vivienda { get; set; }
    
        public virtual Corretaje Corretaje { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Cliente Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recordatorio> Recordatorio { get; set; }
    }
}
