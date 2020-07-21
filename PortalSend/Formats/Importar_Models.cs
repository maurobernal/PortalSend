using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileHelpers;

namespace PortalSend.Formats
{

    [DelimitedRecord(";")]
    public class Importar_Models
    {
        public string titular { get; set; }
        public string tel1 { get; set; }
    }
}