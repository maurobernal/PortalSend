//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PortalSend.App_Data.PORTALSEND
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contactos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contactos()
        {
            this.Mensajes = new HashSet<Mensajes>();
        }
    
        public int con_id { get; set; }
        public string con_titular { get; set; }
        public string con_phone { get; set; }
        public System.DateTime con_fecha { get; set; }
        public string con_lote { get; set; }
        public int con_cant { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mensajes> Mensajes { get; set; }
    }
}
