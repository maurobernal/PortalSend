using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace PortalSend.Models
{
    public class Resultados_Models
    {
        public int res_id { get; set; }
        public string res_mensaje { get; set; }
        public string res_excepcion { get; set; }
        public int res_cantidad { get; set; }
        public string res_metodo { get; set; }
        public string res_tabla { get; set; }
        public ReturnValue res_contenido { get; set; }
    }
}