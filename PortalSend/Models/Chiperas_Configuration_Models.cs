using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PortalSend.Models
{
    public class Chiperas_Configuration_Models
    {


        private static string _UserName = ConfigurationManager.AppSettings["userAdmin"].ToString();
        private static string _PassName = ConfigurationManager.AppSettings["userPass"].ToString();
        private static string _UrlBase = ConfigurationManager.AppSettings["baseUrl"].ToString();
        private static string _PortDefault = ConfigurationManager.AppSettings["portdefault"].ToString();
        private static string _TelReporte = ConfigurationManager.AppSettings["reporte"].ToString();
        public string User_Name { get; set; }
        public string Pass_Name { get; set; }
        public string URL_Base { get; set; }
        public string Port_Default { get; set; }
        public string Tel_Reporte { get; set; }
        public Chiperas_Configuration_Models()
        {
            User_Name = _UserName;
            Pass_Name = _PassName;
            URL_Base = _UrlBase;
            Port_Default = _PortDefault;
            Tel_Reporte = _TelReporte;
        }
    }
}