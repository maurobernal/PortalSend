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
    
    public partial class Mensajes
    {
        public int men_id { get; set; }
        public string men_phone { get; set; }
        public System.DateTime men_fecha { get; set; }
        public string men_estado { get; set; }
        public string men_cuerpo { get; set; }
        public int men_cant { get; set; }
        public System.DateTime men_fechamodif { get; set; }
        public int mencon_id { get; set; }
        public string men_lote { get; set; }
        public string men_enviolote { get; set; }
        public int men_taskid { get; set; }
        public string men_resultado { get; set; }
    
        public virtual Contactos Contactos { get; set; }
    }
}
