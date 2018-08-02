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
    using System.ComponentModel.DataAnnotations;

    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Existencias = new HashSet<Existencias>();
        }
    
        public int Id { get; set; }

        
        public string usu_username { get; set; }

        [RegularExpression("^([a-zA-Z0-9]+\\S*)[@][a-zA-Z0-9]+[.][a-zA-Z]+$", ErrorMessage = "Verificar estructura correo")]
        public string usu_correo { get; set; }

        [RegularExpression("^[a-zA-Z]+\\s*([a-zA-Z]*\\s*)*$", ErrorMessage = "Verificar Nombre")]
        public string usu_nombre { get; set; }

        public string usu_password { get; set; }

        [RegularExpression("^[a-zA-Z]+\\s*([a-zA-Z]*\\s*)*$", ErrorMessage = "Verificar Apellido")]
        public string usu_apellidoPa { get; set; }

        [RegularExpression("^[a-zA-Z]+\\s*([a-zA-Z]*\\s*)*$", ErrorMessage = "Verificar Apellido")]
        public string usu_apellidoMa { get; set; }

        public Nullable<System.DateTime> usu_alta { get; set; }
        public string usu_tipo { get; set; }
        public Nullable<bool> usu_activo { get; set; }
        public Nullable<int> Id_TipoUsiario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Existencias> Existencias { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
