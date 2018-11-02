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
    
    public partial class Corretaje
    {
        public int Id { get; set; }
        public string Crt_Status { get; set; }
        public Nullable<System.DateTime> Crt_FechaAlta { get; set; }
        public string Crt_Cliente_Nombre { get; set; }
        public string Crt_Cliente_ApMat { get; set; }
        public string Crt_Cliente_ApPat { get; set; }
        public string Crt_Direccion { get; set; }
        public string Crt_Nss { get; set; }
        public string Crt_Precio { get; set; }
        public string Crt_Gasto { get; set; }
        public string Crt_Tipo_Vivienda { get; set; }
        public Nullable<int> Crt_Nivel { get; set; }
        public Nullable<int> Crt_Num_Habitaciones { get; set; }
        public Nullable<int> Crt_Planta { get; set; }
        public string Crt_Ano_compra { get; set; }
        public string Crt_Num_Credito_Infonavit { get; set; }
        public Nullable<decimal> Crt_Saldo_infonavit { get; set; }
        public Nullable<System.DateTime> Crt_Fec_Nac { get; set; }
        public string Crt_Tel_Celular { get; set; }
        public string Crt_Estado_Civil { get; set; }
        public string Crt_Tel_Casa { get; set; }
        public string Crt_Tel_Trabajo { get; set; }
        public string Crt_Tel_Ref1 { get; set; }
        public string Crt_Tel_Ref2 { get; set; }
        public string Crt_Tel_Ref { get; set; }
        public string Crt_Recibo_predial_digital { get; set; }
        public string Crt_Clave_Catastral { get; set; }
        public Nullable<decimal> Crt_Adeudo_predial { get; set; }
        public string Crt_Num_servicio_luz { get; set; }
        public Nullable<decimal> Crt_Adeudo_luz { get; set; }
        public string Crt_NombreC_Titular_luz { get; set; }
        public string Crt_No_cuenta_agua { get; set; }
        public Nullable<decimal> Crt_Adeudo_agua { get; set; }
        public Nullable<bool> Crt_Ine_Titu { get; set; }
        public Nullable<bool> Crt_Ine_Conyu { get; set; }
        public Nullable<bool> Crt_Escritura_Simple { get; set; }
        public Nullable<bool> Crt_Acuerdo { get; set; }
        public Nullable<bool> Crt_ActaNacTitu { get; set; }
        public Nullable<bool> Crt_ActaNacConyu { get; set; }
        public Nullable<bool> Crt_ActaMatr { get; set; }
        public Nullable<bool> Crt_EscrCert { get; set; }
        public Nullable<bool> Crt_CartaDesPre { get; set; }
        public string Crt_ReciboLuz { get; set; }
        public string Crt_ReciboAgua { get; set; }
        public string Crt_Otros { get; set; }
        public string Crt_Status_Muestra { get; set; }
        public string Crt_Obervaciones { get; set; }
        public Nullable<int> Crt_ProgresoForm { get; set; }
        public Nullable<int> Id_Vendedor { get; set; }
        public Nullable<int> Id_Usuario { get; set; }
    }
}
