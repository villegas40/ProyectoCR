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
    
    public partial class Contaduria
    {
        public int Id { get; set; }
        public Nullable<decimal> Cnt_M_Preguntar { get; set; }
        public Nullable<decimal> Cnt_Material { get; set; }
        public Nullable<decimal> Cnt_Vigilancia { get; set; }
        public Nullable<decimal> Cnt_Tramites { get; set; }
        public Nullable<decimal> Cnt_CESPT { get; set; }
        public Nullable<decimal> Cnt_CFE { get; set; }
        public Nullable<int> Id_Corretaje { get; set; }
        public Nullable<int> Id_Usuario { get; set; }
    
        public virtual Corretaje Corretaje { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
